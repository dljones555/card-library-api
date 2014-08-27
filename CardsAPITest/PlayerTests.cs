using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardGameLibrary;

namespace CardGameTests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void TestPlayerHandActions()
        {
            var g = new Game();
            var p = new Player { FirstName = "Suzy", LastName = "Watkins" };
            g.Players.Add(p);

            Assert.AreEqual(g.Players.Count, 1);

            p.Hand = g.Deck.Deal(5);

            Assert.AreEqual(p.Hand.Count, 5);
            p.Draw(g.Deck, 2);

            Assert.AreEqual(p.Hand.Count, 7);

            Card c1 = p.Hand[0];

            p.Discard(g.Deck, c1);

            Assert.AreEqual(p.Hand.Count, 6);
            Assert.AreEqual(g.Deck.DiscardPile.Count, 1);
            Assert.AreEqual(g.Deck.DiscardPile[0], c1);
            Assert.AreEqual(g.Deck.AvailableCards.Count, 45);
            Assert.AreEqual(g.Deck.DealtCards.Count, 7);

            Card c2 = p.GetBestCardInHand();
            p.Play(c2);
            Assert.AreEqual(p.PlayedCards.Count, 1);

            var cardRank = c2.Rank;

            Assert.AreEqual(c2.Rank, p.Score);
            Assert.AreEqual(p.Hand.Count, 5);
            Assert.AreEqual(p.IsOutOfCards(), false);

            for (int i = 0; i < 5; i++)
                p.Discard(g.Deck, p.Hand[0]);

            Assert.AreEqual(p.IsOutOfCards(), true); 
        }
    }
}
