using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameLibrary
{
    public interface ICard
    {
        string Name { get; set; }
        string Suit { get; set; }
        int Rank { get; set; }
        Face FaceSide { get; set; }
    }
}
