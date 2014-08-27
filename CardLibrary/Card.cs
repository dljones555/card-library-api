using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameLibrary
{
    public class Card : ICard
    {
        public string Name
        {
            get;
            set;
        }

        public string Suit
        {
            get;    
            set;
        }

        public int Rank
        {
            get;
            set;
        }

        public Face FaceSide
        {
            get;
            set;
        }

    }
}
