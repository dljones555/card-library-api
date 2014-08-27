using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardGameLibrary;

namespace CardGameTests
{
    [TestClass]
    public class CardTests
    {
        [TestMethod]
        public void AddCard()
        {
            var c = new Card { Name = "Jack", Suit = "Spades", Rank = 11 };
            var deck = new Deck<Card>();
            deck.AvailableCards.Add(c);

            Assert.AreEqual(deck.AvailableCards.Exists(d => d.Equals(c)), true);
        }

        [TestMethod]
        public void RemoveCard()
        {
            var deck = new Deck<Card>();
            var c = new Card { Name = "Jack", Suit = "Spades", Rank = 11 };
            deck.Remove(c);

            Assert.AreEqual(deck.AvailableCards.Exists(d => d.Equals(c)), false);
        }
    }
}
