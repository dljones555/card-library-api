using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameLibrary
{

    public class Deck<T> : IDeck<T> where T : Card
    {
        private int _deckSize;
        public bool IsShuffled { get; set; }

        public Deck()
        {
            this.Initialize();
            MakeAcesHigh = false;
        }

        public Deck(bool makeAcesHigh)
        {
            this.MakeAcesHigh = makeAcesHigh;
            this.Initialize();
        }

        public virtual void Initialize()
        {
            IsShuffled = false;

            AvailableCards = new List<T>();
            DealtCards = new List<T>();
            DiscardPile = new List<T>();

            this.BuildDeck();    
            this.Shuffle();
        }

        public virtual void BuildDeck()
        {
            AvailableCards = BuildStandardDeck();
            _deckSize = AvailableCards.Count;
        }

        public virtual List<T> BuildStandardDeck()
        {
            List<T> cards = new List<T>();

            this.BuildCardsInSuit("Spades", ref cards);
            this.BuildCardsInSuit("Hearts", ref cards);
            this.BuildCardsInSuit("Diamonds", ref cards);
            this.BuildCardsInSuit("Clubs", ref cards);

            return cards;
        }

        public virtual void BuildCardsInSuit(string suit, ref List<T> cards)
        {
            var c = new Card { Name = "Ace", Suit = suit, Rank = MakeAcesHigh ? 11 : 1 } as T;
            cards.Add(c);

            for (int i = 2; i <= 10; i++)
            {
                c = new Card { Name = i.ToString(), Suit = suit, Rank = i } as T;
                cards.Add(c);
            }

            c = new Card { Name = "Jack", Suit = suit, Rank = 11 } as T;
            cards.Add(c);

            c = new Card { Name = "Queen", Suit = suit, Rank = 12 } as T;
            cards.Add(c);

            c = new Card { Name = "King", Suit = suit, Rank = 13 } as T;
            cards.Add(c);
        }

        public virtual void Shuffle()
        {
            if (AvailableCards.Count != _deckSize)
            {
                throw new Exception("Deck not full exception.");
            }

            var shuffledDeck = new Dictionary<int, T>();

            Random r = new Random();
            bool findNextPos = true;

            var deckSize = AvailableCards.Count;

            for (int i = 0; i < deckSize; i++)
            {
                findNextPos = true;
                do
                {
                    int deckPos = r.Next(0, deckSize);
          
                    if (!shuffledDeck.ContainsKey(deckPos))
                    {
                        //Console.WriteLine(deckPos);
                        shuffledDeck.Add(deckPos, AvailableCards[i]);
                        findNextPos = false;
                    }

                } while (findNextPos == true);
            }

            foreach (var shuffledCard in shuffledDeck.OrderBy(i => i.Key).ToList())
            {
                var index = shuffledCard.Key;
                var card = shuffledCard.Value;
                AvailableCards[index] = card;
            }

            IsShuffled = true;
        }

        public virtual List<T> Deal(int numCards)
        {
            if (AvailableCards.Count == 0)
                throw new Exception("Deck out of cards exception.");

            if (numCards > AvailableCards.Count)
                throw new Exception("No cards left to deal exception.");

            var cards = AvailableCards.Take(numCards).ToList();
            cards.ToList().ForEach(c => { AvailableCards.Remove(c); });
            DealtCards.AddRange(cards);

            return cards;
        }

        public virtual void Discard(T card)
        {
            DiscardPile.Add(card);
        }

        public List<T> AvailableCards
        {          
            get; set;
        }

        public List<T> DealtCards
        {
            get; set;
        }

        public List<T> DiscardPile
        {
            get; set;
        }

        public void ResetDeck()
        {
            Initialize();
        }

        public void Remove(T card)
        {
            var cardToRemove = AvailableCards.Find(c => c.Rank == card.Rank &&
                                                        c.Name == card.Name &&
                                                        c.Suit == card.Suit);
            AvailableCards.Remove(cardToRemove);
        }

        public void Remove(int numCards)
        {
            AvailableCards.RemoveRange(0, numCards);
        }

        public bool MakeAcesHigh
        {
            get;
            set;
        }
    }
}
