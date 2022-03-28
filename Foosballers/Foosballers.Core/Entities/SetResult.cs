namespace Foosballers.Core.Entities;

public class SetResult
{
    public bool IsFinished { get; }
    public Player? Winner { get; }
    public SetScore SetScore { get; }

    private SetResult(bool isFinished, Player winner)
    {
        IsFinished = isFinished;
        Winner = winner;
    }
    private SetResult(bool isFinished)
    {
        IsFinished = isFinished;
    }
    private SetResult(SetScore setScore)
    {
        SetScore = setScore;
    }

    internal static SetResult Finished(Player winner)
    {
        return new SetResult(true, winner);
    }
    internal static SetResult Pending()
    {
        return new SetResult(false);
    }

    public static SetResult New(Player first, Player second)
    {
        return new SetResult(new SetScore(PlayerScore.Empty(first),PlayerScore.Empty(second)));
    }
}