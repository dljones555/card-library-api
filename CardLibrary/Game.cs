using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameLibrary
{
    public class Game : IGame
    {
        public Game()
        {
            Players = new List<Player>();
            Deck = new Deck<Card>();
        }

        public List<Player> Players
        {
            get; set;
        }

        public Deck<Card> Deck
        {
            get;
            set;
        }

        public virtual int GetPlayedCardsScore(List<Card> cards)
        {
            var score = cards.Sum(c => c.Rank);
            return score;
        }

        public virtual void Play()
        {
            throw new NotImplementedException();
        }

        public virtual List<Tuple<Player,int>> DetermineGameWinner()
        {
            var scores = new List<Tuple<Player, int>>();
            var winners = Players.ToList().OrderByDescending(p => GetPlayedCardsScore(p.PlayedCards.ToList())).
                          Select(p => new Tuple<Player,int>(p,GetPlayedCardsScore(p.PlayedCards.ToList())));

            var topOfList = winners.FirstOrDefault();

            List<Tuple<Player, int>> winnerList = winners.Where(w => topOfList.Item2 == w.Item2).ToList();

            return winnerList;
        }

        public virtual List<Tuple<Player,Card>> CompareLastCardPlayed()
        {
            var compared = new List<Tuple<Player, Card>>();

            var winners = Players.ToList().OrderByDescending(p => p.PlayedCards.Peek().Rank).
                          Select(p => new Tuple<Player, Card>(p, p.PlayedCards.Peek()));

            var topOfList = winners.FirstOrDefault();

            List<Tuple<Player, Card>> winnerList = winners.Where(w => topOfList.Item2.Rank == w.Item2.Rank).ToList();

            return winnerList;
        }

        public virtual List<Tuple<Player, List<Card>>> CompareLastCardsPlayed()
        {
            var compared = new List<Tuple<Player, List<Card>>>();

            var winners = Players.ToList().OrderByDescending(p => p.PlayedCards.Take(p.LastPlayCount).Sum(a => a.Rank)).
                          Select(p => new Tuple<Player, List<Card>>(p, p.PlayedCards.Take(p.LastPlayCount).ToList()));
            var topOfList = winners.FirstOrDefault();

            var winnerList = winners.Where(w => topOfList.Item2.Sum(r => r.Rank) == w.Item2.Sum(s => s.Rank)).ToList();

            return winnerList;
        }
    }
}
