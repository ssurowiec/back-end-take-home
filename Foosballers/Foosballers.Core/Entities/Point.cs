namespace Foosballers.Core.Entities;

internal class Point
{
    public Player Scorer { get; }

    private Point(Player scorer)
    {
        Scorer = scorer;
    }

    internal static Point AchieveBy(Player player)
    {
        return new Point(player);
    }
}