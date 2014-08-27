using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameLibrary
{
    public interface IGame
    {
        List<Player> Players { get; set; }
        Deck<Card> Deck { get; set; }
        int GetPlayedCardsScore(List<Card> cards);
        List<Tuple<Player,int>> DetermineGameWinner();
        List<Tuple<Player,Card>> CompareLastCardPlayed();
        List<Tuple<Player, List<Card>>> CompareLastCardsPlayed();
        // Idea: make this Play Func based with an int completion value
        void Play();
    }
}
