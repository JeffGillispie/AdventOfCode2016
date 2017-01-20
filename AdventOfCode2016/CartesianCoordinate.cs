using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    public class CartesianCoordinate
    {
        private int x;
        private int y;

        public CartesianCoordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X
        {
            get
            {
                return this.x;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
        }

        public void Move(Direction.Cardinal direction, int distance)
        {
            switch(direction)
            {
                case Direction.Cardinal.North:
                    this.y += distance;
                    break;
                case Direction.Cardinal.East:
                    this.x += distance;
                    break;
                case Direction.Cardinal.South:
                    this.y -= distance;
                    break;
                case Direction.Cardinal.West:
                    this.x -= distance;
                    break;
                default:
                    throw new Exception("Invalid Direction");
            }
        }

        public void Move(Direction.Relative2D direction, string[,] keypad)
        {
            CartesianCoordinate backup = new CartesianCoordinate(this.x, this.y);
            // position is relative to a 2D array
            try
            {
                switch (direction)
                {
                    case Direction.Relative2D.U:
                        this.y -= 1;
                        break;
                    case Direction.Relative2D.D:
                        this.y += 1;
                        break;
                    case Direction.Relative2D.L:
                        this.x -= 1;
                        break;
                    case Direction.Relative2D.R:
                        this.x += 1;
                        break;
                    default:
                        throw new Exception("Invalid Direction");
                }
                // test to see if the position is valid
                string test = keypad[this.y, this.x];
                // test to see if the value is valid
                if (test == null)
                {
                    throw new Exception("Invalid Value");
                }
            }
            catch(Exception)
            {
                // reset the position to the backup position
                this.x = backup.x;
                this.y = backup.y;
            }
        }

        public List<CartesianCoordinate> GetMoves(Direction.Cardinal direction, int distance)
        {
            List<CartesianCoordinate> moves = new List<CartesianCoordinate>();
            
            for (int i = 1; i <= distance; i++)
            {
                CartesianCoordinate position = new CartesianCoordinate(this.x, this.y);
                position.Move(direction, i);
                moves.Add(position);
            }

            return moves;
        }
    }
}
