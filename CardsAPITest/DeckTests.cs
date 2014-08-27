using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardGameLibrary;

namespace CardGameTests
{
    [TestClass]
    public class DeckTests
    {
        [TestMethod]
        public void CheckForFullDeck()
        {
            var deck = new Deck<Card>();
            Assert.AreEqual(deck.AvailableCards.Count, 52);
        }

        [TestMethod]
        public void VerifyShuffled() 
        {
            var deck = new Deck<Card>();

            var standardDeck = deck.BuildStandardDeck();
            bool isShuffled = true;

            foreach (var card in standardDeck)
            {
                if (deck.AvailableCards.Exists(c => c.Name == card.Name && c.Suit == card.Suit && c.Rank == card.Rank))
                {
                    isShuffled = true;
                    break;
                }
            }

            Assert.AreEqual(isShuffled, true);
        }

        [TestMethod]
        public void TestNumberDealt()
        {
            var deck = new Deck<Card>();
            var cards = deck.Deal(5);

            Assert.AreEqual(cards.Count, 5);
        }

        [TestMethod]
        public void DealCards()
        {
            int numCards = 11;
            var deck = new Deck<Card>();
            var hand = deck.Deal(numCards);

            Assert.AreEqual(hand.Count, numCards);
            Assert.AreEqual(deck.AvailableCards.Count, 52 - hand.Count);
        }

    }
}
