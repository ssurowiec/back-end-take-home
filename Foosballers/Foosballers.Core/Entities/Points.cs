namespace Foosballers.Core.Entities;

internal class Points
{
    private readonly ICollection<Point> _points;
    
    internal IEnumerable<PointStatus> Status => _points.GroupBy(p => p.Scorer,new PlayerEqualityComparer()).Select(gp => new PointStatus(gp.Key,gp.Count()));
    public Points(int amount)
    {
        _points = new List<Point>(amount);

    }
    
    internal int AchieveBy(Player player)
    {
        _points.Add(Point.AchieveBy(player));
        return _points.Count(p => p.Scorer.Equals(player));
    }

    internal IEnumerable<(Player,int)> GetPointSummary()
    {
        return _points.GroupBy(p => p.Scorer,new PlayerEqualityComparer()).Select(gp => (gp.Key,gp.Count()));
    }
}

internal class PointStatus
{
    public Player Player { get; }
    public int Points { get; private set; }

    public PointStatus(Player player, int points)
    {
        Player = player;
        Points = points;
    }

    internal void UpdatePoints(int points)
    {
        Points = points;
    }
}