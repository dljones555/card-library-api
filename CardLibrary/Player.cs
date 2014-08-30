using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameLibrary
{
    public class Player : IPlayer
    {
        public Player()
        {
            PlayedCards = new Stack<Card>();
            PickPile = new Stack<Card>();
            LastPlayCount = 0;
        }

        public string FirstName
        {
            get; set;
        }

        public string LastName
        {
            get; set;
        }

        public List<Card> Hand
        {
            get; set;
        }

        public Stack<Card> PlayedCards
        {
            get; set;
        }

        public Stack<Card> PickPile
        {
            get; set;
        }

        public int LastPlayCount
        {
            get; set;
        }

        public int Score
        {
            get
            {
                return GetPlayedTotal();
            }
        }

        public virtual void Draw(Deck<Card> deck, int numCards)
        {
            List<Card> cards = deck.Deal(numCards);

            cards.ForEach(c => {
                Hand.Add(c);
            });      
        }

        public virtual void Discard(Deck<Card> deck, Card card)
        {
            var discardCard = Hand.Find(c => c.Rank == card.Rank && 
                                        c.Name == card.Name && 
                                        c.Suit == card.Suit);
            deck.DiscardPile.Add(discardCard);
            Hand.Remove(discardCard);
        }

        public virtual void Discard(Deck<Card> deck, List<Card> cards)
        {
            cards.ForEach(c => {
                this.Discard(deck, c);
            });
        }

        public void RemoveCardFromHand(List<Card> hand, Card card)
        {
            var removeCard = Hand.Find(c => c.Rank == card.Rank &&
                                            c.Name == card.Name &&
                                            c.Suit == card.Suit);
            Hand.Remove(removeCard);
        }

        public virtual void Play(Card card)
        {
            PlayedCards.Push(card);
            RemoveCardFromHand(Hand, card);
            LastPlayCount = 1;
        }

        public virtual void Play(List<Card> cards)
        {
            cards.ForEach(c =>
            {
                PlayedCards.Push(c);
                RemoveCardFromHand(Hand, c);
            });

            LastPlayCount = cards.Count;
        }

        public virtual Card GetBestCardInHand()
        {
            var best = Hand.OrderByDescending(c => c.Rank);
            return best.FirstOrDefault();
        }

        public virtual bool CheckFor(List<Card> cards)
        {
            throw new NotImplementedException();
        }

        public virtual int GetPlayedTotal()
        {
            var sum = PlayedCards.Sum(s => s.Rank);
            return sum;
        }
        
        public virtual bool IsOutOfCards()
        {
            return this.Hand.Count == 0;
        }

    }
}
