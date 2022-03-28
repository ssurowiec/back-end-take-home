using Foosballers.Core;
using Foosballers.Core.Entities;

namespace Foosballers.Services;

internal class CreateGameService : ICreateGameService
{
    private readonly IGameRepository _gameRepository;

    public CreateGameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<CreateGameResult> CreateGameAsync(CreateGame command)
    {
        if (!PlayersAreValid(command.FirstPlayer, command.SecondPlayer))
        {
            return CreateGameResult.Failed("Players are not valid");
        }

        if (await PlayersHaveActiveGame(command.FirstPlayer, command.SecondPlayer))
        {
            return CreateGameResult.Failed("Players have active games");
        }

        const int numberOfSets = 2;
        const int numberOfPointsInSet = 10;
        var gameFactory = new GameFactory(
            command.FirstPlayer, 
            command.SecondPlayer, 
            new GameRules(numberOfSets, numberOfPointsInSet)
            );
        var game = gameFactory.Start();
        
        await _gameRepository.SaveAsync(game);
        
        return CreateGameResult.Ok();
    }

    private bool PlayersAreValid(string firstPlayer, string secondPlayer)
    {
        if (string.IsNullOrWhiteSpace(firstPlayer)
            || string.IsNullOrWhiteSpace(secondPlayer))
        {
            return false;
        }

        return !firstPlayer.Equals(secondPlayer);
    }
    private async Task<bool> PlayersHaveActiveGame(Player firstPlayer, Player secondPlayer)
    {
        var first = await _gameRepository.FindActiveAsync(firstPlayer);
        var second = await _gameRepository.FindActiveAsync(secondPlayer);

        return first is not null || second is not null;
    }
}

public interface ICreateGameService
{
    Task<CreateGameResult> CreateGameAsync(CreateGame command);
}

public class CreateGameResult
{
    public bool Success { get; }
    public string ErrorMessage { get; }

    private CreateGameResult(bool success, string errorMessage)
    {
        Success = success;
        ErrorMessage = errorMessage;
    }

    private CreateGameResult(bool success)
    {
        Success = success;
        ErrorMessage = string.Empty;
    }

    public static CreateGameResult Failed(string errorMessage)
        => new CreateGameResult(false, errorMessage);
    public static CreateGameResult Ok()
        => new CreateGameResult(true);
}

public class CreateGame
{
    public string FirstPlayer { get; }
    public string SecondPlayer { get; }

    public CreateGame(string firstPlayer, string secondPlayer)
    {
        FirstPlayer = firstPlayer;
        SecondPlayer = secondPlayer;
    }
}