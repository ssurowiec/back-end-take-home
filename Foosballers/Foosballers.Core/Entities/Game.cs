namespace Foosballers.Core.Entities;

public sealed class Game
{
    public Guid Id { get; }
    private readonly int _maximumAmountOfSets;
    private readonly Sets _sets;
    public Player First { get; }
    public Player Second { get; }
    public GameResult GameResult { get; }
    public DateTime CreatedAt { get; }

    public GameResult Score(Player player)
    {
        if (!PlayerAttendToGame(player) || GameResult.IsFinished)
        {
            return GameResult;
        }
        
        var set = _sets.Score(player);
        GameResult.UpdateSetResults(_sets.Results);
        if (!set.IsFinished)
        {
            return GameResult;
        }
        var wonByPlayer = _sets.WonByPlayer(player);
        if (wonByPlayer.Equals(_maximumAmountOfSets))
        {
            GameResult.Finish(player,_sets.Results);
            return GameResult;
        }


        _sets.BeginNew(First,Second);
        GameResult.UpdateSetResults(_sets.Results);
        return GameResult;
    }

    private Game(Player first, Player second, int amountOfSet, int amountOfPointsInSet)
    {
        First = first;
        Second = second;
        _maximumAmountOfSets = amountOfSet;
        _sets = new Sets(amountOfSet,amountOfPointsInSet,First,Second);
        GameResult = new GameResult();
        GameResult.UpdateSetResults(_sets.Results);
        CreatedAt = DateTime.UtcNow;
        Id = Guid.NewGuid();
    }
    private bool PlayerAttendToGame(Player player)
        => player.Equals(First) || player.Equals(Second);

    internal static Game Start(Player first, Player second, int amountOfSets, int amountOfPointsInSet)
    {
        return new Game(first, second,amountOfSets,amountOfPointsInSet);
    }

}