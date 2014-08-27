using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameLibrary
{
    public interface IDeck<T>
    {
        List<T> AvailableCards { get; set; }
        List<T> DealtCards { get; set; }
        List<T> DiscardPile { get; set; }

        bool MakeAcesHigh { get; set; }
        bool IsShuffled { get; set; }
        void Remove(T card);
        void Remove(int numCards);
        void Shuffle();
        List<T> Deal(int numCards);
        void Discard(T card);
        void ResetDeck();
    }
}
