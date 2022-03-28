using System.Linq;
using Foosballers.Core.Entities;
using NUnit.Framework;

namespace Foosballers.Core.Tests;

public class GameTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GivenPlayers_WhenPlayerWonGame_ShouldChangeGameResultAndSetWinnerCorrectly()
    {
        var firstPlayer = "first";
        var secondPlayer = "second";
        var gameFactory = new GameFactory(firstPlayer, secondPlayer, new GameRules(2, 10));
        var game = gameFactory.Start();
        GameResult gameResult = null;
        for (var i = 0; i < 10; i++)
        {
            gameResult = game.Score(firstPlayer);
        }
        for (var i = 0; i < 10; i++)
        {
            gameResult = game.Score(secondPlayer);
        }
        for (var i = 0; i < 10; i++)
        {
            gameResult = game.Score(firstPlayer);

        }
        for (var i = 0; i < 10; i++)
        {
            gameResult = game.Score(firstPlayer);
        }

        Assert.AreEqual(firstPlayer, gameResult.Winner.ToString());
        Assert.True(gameResult.IsFinished);
        Assert.AreEqual(2,gameResult.SetResults.Count(sr => sr.Winner.Equals(firstPlayer)));
        Assert.AreEqual(1,gameResult.SetResults.Count(sr => sr.Winner.Equals(secondPlayer)));
    }
}