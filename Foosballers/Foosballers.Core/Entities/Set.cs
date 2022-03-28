namespace Foosballers.Core.Entities;

internal class Set
{
    private readonly int _maximalAmountOfPoints;
    private readonly Points _points;
    public Guid Id { get; }
    public SetResult Result { get; private set; }

    private Set(int amountOfPoints,Player first,Player second)
    {
        _maximalAmountOfPoints = amountOfPoints;
        var capacityOfPoints = _maximalAmountOfPoints + (_maximalAmountOfPoints - 1);
        _points = new Points(capacityOfPoints);
        Result = SetResult.New(first,second);
        Id = Guid.NewGuid();
    }
    internal SetResult Score(Player player)
    {
        var pointsAmount = _points.AchieveBy(player);
        UpdateSetScore();
        if (pointsAmount.Equals(_maximalAmountOfPoints))
        {
            Result = SetResult.Finished(player);
            return Result;
        }
        return SetResult.Pending();
    }

    private void UpdateSetScore()
    {
        var pointsStatus = _points.Status;
        foreach (var status in pointsStatus)
        {
            Result.SetScore.ChangeScore(status.Player,status.Points);
        }
    }
    internal static Set Begin(int amountOfPoints,Player first, Player second) => new(amountOfPoints, first,second);

}