namespace Foosballers.Core.Entities;

internal class Sets
{
    private readonly int _amountOfPointsInSet;
    private readonly ICollection<Set> _sets;
    private Set? Active => _sets.SingleOrDefault(s => !s.Result.IsFinished);
    public IEnumerable<SetResult> Results => _sets.Select(s => s.Result);

    internal Sets(int maximumAmountOfSets,int amountOfPointsInSet,Player first, Player second)
    {
        _amountOfPointsInSet = amountOfPointsInSet;
        var capacityOfSets = maximumAmountOfSets + (maximumAmountOfSets - 1);
        _sets = new List<Set>(capacityOfSets);
        _sets.Add(Set.Begin(amountOfPointsInSet,first,second));
    }

    internal SetResult Score(Player scorer)
        => Active?.Score(scorer);

    internal int WonByPlayer(Player player)
    {
        return _sets.Count(s => s.Result.IsFinished && s.Result.Winner is not null && s.Result.Winner.Equals(player));
    }
    internal void BeginNew(Player first, Player second)
    {
        if (_sets.Any(s => !s.Result.IsFinished))
        {
            return;
        }
        var newSet = Set.Begin(_amountOfPointsInSet,first,second);
        _sets.Add(newSet);
    }

}