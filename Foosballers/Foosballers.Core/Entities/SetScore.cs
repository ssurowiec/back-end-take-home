namespace Foosballers.Core.Entities;

public class SetScore
{
    public PlayerScore FirstPlayerScore { get; }
    public PlayerScore SecondPlayerScore { get; }

    internal SetScore(PlayerScore firstPlayerScore, PlayerScore secondPlayerScore)
    {
        FirstPlayerScore = firstPlayerScore;
        SecondPlayerScore = secondPlayerScore;
    }

    internal void ChangeScore(Player player, int amount)
    {
        if (FirstPlayerScore.Player.Equals(player))
        {
            FirstPlayerScore.ChangePoints(amount);
            return;
        }

        SecondPlayerScore.ChangePoints(amount);
    }
}