namespace Foosballers.Core.Entities;

public class GameResult
{
    private readonly List<SetResult> _setResults = new List<SetResult>();
    public bool IsFinished { get; private set; }
    public Player? Winner { get; private set; }
    public IEnumerable<SetResult> SetResults => _setResults;

    internal void UpdateSetResults(IEnumerable<SetResult> setResults)
    {
        _setResults.Clear();
        _setResults.AddRange(setResults);
    }

    public void Finish(Player winner, IEnumerable<SetResult> setResults)
    {
        IsFinished = true;
        Winner = winner;
        UpdateSetResults(setResults);
    }
}