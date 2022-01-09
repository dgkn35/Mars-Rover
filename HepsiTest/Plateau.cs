using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiTest
{
    public class Plateau
    {
        public readonly int MinX = 0;
        public readonly int MinY = 0;
        public int MaxX { get; set; }
        public int MaxY { get; set; }

        public Plateau(int maxX, int maxY)
        {
            MaxX = maxX;
            MaxY = maxY;
        }
    }
}
