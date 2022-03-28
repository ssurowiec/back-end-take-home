using Foosballers.Core.Entities;

namespace Foosballers.Core;

public interface IGameRepository
{
    Task<Game?> FindActiveAsync(Player player);
    Task<IEnumerable<Game>> FindAll(Player player);
    Task SaveAsync(Game game);
}