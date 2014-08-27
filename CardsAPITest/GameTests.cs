using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardGameLibrary;

namespace CardGameTests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void TestCompareLastCardsPlayed()
        {
            Game game = new Game();

            Player player1 = new Player { FirstName = "David", LastName = "Jones" };
            Player player2 = new Player { FirstName = "Snuffy", LastName = "Smith" };

            game.Players.Add(player1);
            game.Players.Add(player2);

            player1.Hand = game.Deck.Deal(10);
            player2.Hand = game.Deck.Deal(10);

            var p1Play = new List<Card>();
            p1Play.Add(player1.Hand[0]);
            p1Play.Add(player1.Hand[1]);

            var p2Play = new List<Card>();
            p2Play.Add(player2.Hand[0]);
            p2Play.Add(player2.Hand[1]);

            player1.Play(p1Play);
            player2.Play(p2Play);

            var result = game.CompareLastCardsPlayed();

            bool isValidWin = false;

            if (result.Count == 1)
            {
                if (result[0].Item1 == player1)
                {
                    isValidWin = player1.Score > player2.Score;
                }
                else if (result[0].Item1 == player2)
                {
                    isValidWin = player2.Score > player1.Score;
                }
            }
            else if (result.Count == 2)
            {
                isValidWin = player1.Score == player2.Score;
            }

            Assert.AreEqual(isValidWin, true);

        }
    }
}
