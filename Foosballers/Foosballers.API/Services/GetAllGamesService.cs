using Foosballers.Core;

namespace Foosballers.Services;

public class GetAllGamesService : IGetAllGamesService
{
    private readonly IGameRepository _gameRepository;

    public GetAllGamesService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<GetAllGamesResult> GetAllGames(string player)
    {
        var games = (await _gameRepository.FindAll(player)).ToList();
        if (!games.Any())
        {
            return GetAllGamesResult.NotFound();
        }
        
        return GetAllGamesResult.Ok(games.Select(g =>
            new GameDto
            {
                CreatedAt = g.CreatedAt,
                FirstPlayer = g.First,
                SecondPlayer = g.Second,
                IsFinished = g.GameResult.IsFinished
            }));
    }
    
}

public interface IGetAllGamesService
{
    Task<GetAllGamesResult> GetAllGames(string player);
}

public class GetAllGamesResult
{
    private GetAllGamesResult(bool found, IEnumerable<GameDto> games)
    {
        Found = found;
        Games = games;
    }

    public bool Found { get; }
    public IEnumerable<GameDto> Games { get; }


    public static GetAllGamesResult NotFound()
        => new GetAllGamesResult(false, Enumerable.Empty<GameDto>());
    

    public static GetAllGamesResult Ok(IEnumerable<GameDto> games)
        => new GetAllGamesResult(true, games);
}

public class GameDto
{
    public string FirstPlayer { get; set; }
    public string SecondPlayer { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsFinished { get; set; }
}