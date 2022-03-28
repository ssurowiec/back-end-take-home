using Foosballers.Core;
using Foosballers.Core.Entities;

namespace Foosballers.Services;

public class ScoreService : IScoreService
{
    private readonly IGameRepository _gameRepository;

    public ScoreService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<ScoreResult> Score(string player)
    {
        var game = await _gameRepository.FindActiveAsync(player);
        if (game is null)
        {
            return ScoreResult.Failed("Player has no active game");
        }

        var gameResult = game.Score(player);

        await _gameRepository.SaveAsync(game);
        return ScoreResult.Ok();
    }
}

public interface IScoreService
{
    Task<ScoreResult> Score(string player);
}

public class ScoreResult
{
    public bool Success { get; }
    public string ErrorMessage { get; }

    private ScoreResult(bool success, string errorMessage)
    {
        Success = success;
        ErrorMessage = errorMessage;
    }

    public static ScoreResult Failed(string errorMessage)
        => new ScoreResult(false,errorMessage);
    public static ScoreResult Ok()
        => new ScoreResult(false,string.Empty);
}