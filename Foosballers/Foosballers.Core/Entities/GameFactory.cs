namespace Foosballers.Core.Entities;

public class GameFactory
{
    private readonly Player _first;
    private readonly Player _second;
    private readonly GameRules _gameRules;

    public GameFactory(Player first, Player second, GameRules gameRules)
    {
        _first = first;
        _second = second;
        _gameRules = gameRules;
    }

    public Game Start()
    {
        return Game.Start(_first, _second, _gameRules.AmountOfSets, _gameRules.AmountOfPointsInSet);
    }
}