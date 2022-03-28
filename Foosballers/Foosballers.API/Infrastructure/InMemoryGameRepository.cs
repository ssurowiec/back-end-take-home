using Foosballers.Core;
using Foosballers.Core.Entities;

namespace Foosballers.Infrastructure;

public class InMemoryGameRepository : IGameRepository
{
    private List<Game> _games = new();
    public Task<Game?> FindActiveAsync(Player player)
    {
        return Task.FromResult(_games.FirstOrDefault(g => !g.GameResult.IsFinished &&
                                                          (g.First.Equals(player) || g.Second.Equals(player))));
    }

    public Task<IEnumerable<Game>> FindAll(Player player)
    {
        return Task.FromResult(_games.Where(g => g.First.Equals(player) || g.Second.Equals(player)));
    }

    public Task SaveAsync(Game game)
    {
        if (!_games.Contains(game))
        {
            _games.Add(game);
        }
        return Task.CompletedTask;
    }
}