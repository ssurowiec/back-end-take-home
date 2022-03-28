using System.Text.Json;
using Foosballers.Core;
using Foosballers.Core.Entities;

namespace Foosballers.Infrastructure;

public class FileGameRepository : IGameRepository
{
    private const string FileName = "GAMES.json";

    public async Task<Game?> FindActiveAsync(Player player)
    {
        await using var stream = new FileStream(FileName, FileMode.OpenOrCreate);
        var games = await JsonSerializer.DeserializeAsync<IEnumerable<Game>>(stream);
        return games?.FirstOrDefault(g => !g.GameResult.IsFinished &&
                                          (g.First.Equals(player) || g.Second.Equals(player)));
    }

    public async Task<IEnumerable<Game>> FindAll(Player player)
    {
        await using var stream = new FileStream(FileName, FileMode.OpenOrCreate);
        var games = await JsonSerializer.DeserializeAsync<IEnumerable<Game>>(stream);
        return games is null ? new List<Game>() : games.Where(g => g.First.Equals(player) || g.Second.Equals(player));
    }

    public async Task SaveAsync(Game game)
    {
        await using var stream = new FileStream(FileName, FileMode.OpenOrCreate);
        var games = await JsonSerializer.DeserializeAsync<List<Game>>(stream) ?? new List<Game>();
        var gameInFile = games.FirstOrDefault(g => g.Id.Equals(game.Id));
        if (gameInFile is not null)
        {
            games.Remove(game);
        }
        games.Add(game);
        await JsonSerializer.SerializeAsync(stream, games);

    }
}