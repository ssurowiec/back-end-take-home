namespace Foosballers.Core.Entities;

public class GameRules
{
    public int AmountOfSets { get; }
    public int AmountOfPointsInSet { get; }
    
    public GameRules(int amountOfSets, int amountOfPointsInSet)
    {
        AmountOfSets = amountOfSets;
        AmountOfPointsInSet = amountOfPointsInSet;
    }

}