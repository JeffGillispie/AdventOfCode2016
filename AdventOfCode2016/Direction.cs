using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    public class Direction
    {
        public enum Cardinal
        {
            North = 0,
            East = 1,
            South = 2,
            West = 3
        }

        public enum Relative
        {
            L = -1,
            R = 1
        }

        public enum Relative2D
        {
            U,
            D,
            L,
            R
        }
    }
}
