using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGameLibrary;

namespace CardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            Player player1 = new Player { FirstName = "David", LastName = "Jones" };
            Player player2 = new Player { FirstName = "Snuffy", LastName = "Smith" };

            game.Players.Add(player1);
            game.Players.Add(player2);

            player1.Hand = game.Deck.Deal(10);
            player2.Hand = game.Deck.Deal(10);

            do
            {
                if (player1.IsOutOfCards())
                {
                    Console.WriteLine("Player 1 wins");
                    Console.WriteLine("Player 1 Score: {0}. Player 2 Score: {1}", player1.Score, player2.Score);
                    break;
                }

                if (player2.IsOutOfCards())
                {
                    Console.WriteLine("Player 2 wins");
                    Console.WriteLine("Player 2 Score: {0}. Player 1 Score: {1}", player2.Score, player1.Score);
                    break;
                }

                Card c1 = player1.GetBestCardInHand();            
                WriteMessage(player1, c1, "plays - hand count = " + player1.Hand.Count().ToString());
                player1.Play(c1);

                Card c2 = player2.GetBestCardInHand();       
                WriteMessage(player2, c2, "plays - hand count = " + player2.Hand.Count().ToString());
                player2.Play(c2);

                var handWinner = game.CompareLastCardPlayed();

                if (handWinner.Count == 1)
                {
                    var winner = handWinner[0];
                    winner.Item1.Discard(game.Deck, winner.Item2);

                    WriteMessage(winner.Item1, winner.Item2, "wins");

                    if (winner.Item1 != player1)
                        player1.Draw(game.Deck, 1);

                    if (winner.Item1 != player2)
                        player2.Draw(game.Deck, 1);
                }
                else // tie in this teest
                {
                    Console.WriteLine("Tie");
                    player1.Draw(game.Deck, 1);
                    player2.Draw(game.Deck, 2);
                }

                Console.WriteLine();

            }
            while (game.Deck.AvailableCards.Count > 0);

            Console.ReadLine();

        }

        public static void WriteMessage(Player p, Card c, string msg)
        {
            Console.WriteLine( string.Format("{0} {1} - {2} of {3}({4}) {5}", p.FirstName, p.LastName, c.Name,  c.Suit, c.Rank, msg) );
        }

    }
}
