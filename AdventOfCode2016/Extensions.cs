using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    public static class Extensions
    {
        public static Direction.Cardinal GetDirection(this Direction.Cardinal direction, Direction.Relative turn)
        {
            if (direction.Equals(Direction.Cardinal.North) && turn.Equals(Direction.Relative.L))
            {
                // turn left from north to west
                return Direction.Cardinal.West;
            }
            else if (direction.Equals(Direction.Cardinal.West) & turn.Equals(Direction.Relative.R))
            {
                // turn right from west to north
                return Direction.Cardinal.North;
            }
            else
            {
                // in bounds so add the enums
                return (Direction.Cardinal)((int)direction + (int)turn);
            }
        }
    }
}
