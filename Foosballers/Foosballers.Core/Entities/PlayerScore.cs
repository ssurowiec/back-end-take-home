namespace Foosballers.Core.Entities;

public class PlayerScore
{
    public Player Player { get; }
    public int AmountOfPoints { get; private set; }

    public PlayerScore(Player player, int amountOfPoints)
    {
        Player = player;
        AmountOfPoints = amountOfPoints;
    }

    internal static PlayerScore Empty(Player player)
    {
        return new PlayerScore(player, 0);
    }

    internal void ChangePoints(int amount)
    {
        AmountOfPoints = amount;
    }
}