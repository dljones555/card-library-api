using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameLibrary
{
    public interface IPlayer
    {
        string FirstName { get; set; }
        string LastName { get; set; }
    
        List<Card> Hand { get; set; }
        void Draw(Deck<Card> deck, int numCards);
        void Discard(Deck<Card> deck, Card card);
        void Discard(Deck<Card> deck, List<Card> cards);
        void Play(Card card);
        void Play(List<Card> cards);
        Card GetBestCardInHand();
        bool IsOutOfCards();

        int LastPlayCount { get; set; }
        Stack<Card> PlayedCards { get; set; }
        // PickPile is a player's own facedown pile one works from - this is used in some card games
        Stack<Card> PickPile { get; set; }

        // Intent: implement ability to check for things like straight, pairs, etc. TBD if this makes sense.
        bool CheckFor(List<Card> cards);

        int GetPlayedTotal();
        int Score { get; }
    }
}
