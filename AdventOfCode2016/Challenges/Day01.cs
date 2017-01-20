using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Challenges
{
    public class Day01 : Day
    {
        public string Part1(string input)
        {
            string[] instructions = System.IO.File.ReadAllText(input).Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            CartesianCoordinate position = new CartesianCoordinate(0,0);
            Direction.Cardinal direction = Direction.Cardinal.North;
            // process the instructions
            foreach (string instruction in instructions)
            {
                // parse the direction and the distance from the instruction
                Direction.Relative turn = (Direction.Relative)Enum.Parse(typeof(Direction.Relative), instruction.Substring(0, 1));
                int distance = int.Parse(instruction.Substring(1));
                // move to the new location specified in the instruction
                direction = direction.GetDirection(turn);
                position.Move(direction, distance);
            }
            // return the distance traveled
            // absolute value of the sum of the vertical and horizontal values
            return (Math.Abs(position.X) + Math.Abs(position.Y)).ToString();
        }

        public string Part2(string input)
        {
            string[] instructions = System.IO.File.ReadAllText(input).Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            CartesianCoordinate position = new CartesianCoordinate(0, 0);
            Direction.Cardinal direction = Direction.Cardinal.North;
            HashSet<string> positionHistory = new HashSet<string>();            
            position = processInstructions(instructions, direction, position, positionHistory);
            // return the distance traveled
            return (Math.Abs(position.X) + Math.Abs(position.Y)).ToString();
        }

        private CartesianCoordinate processInstructions(string[] instructions, Direction.Cardinal direction, CartesianCoordinate position, HashSet<string> positionHistory)
        {
            foreach (string instruction in instructions)
            {
                // parse the direction and the distance from the instruction
                Direction.Relative turn = (Direction.Relative)Enum.Parse(typeof(Direction.Relative), instruction.Substring(0, 1));
                int distance = int.Parse(instruction.Substring(1));
                // get new direction and the moves between the current and new positions
                direction = direction.GetDirection(turn);
                List<CartesianCoordinate> moves = position.GetMoves(direction, distance);
                // loop over each move
                foreach (CartesianCoordinate move in moves)
                {
                    position = move;
                    string location = String.Format("{0},{1}", position.X, position.Y);
                    // check if this location is the destination
                    if (positionHistory.Contains(location))
                    {
                        // arrived at a position we have already been to
                        return position;
                    }
                    else
                    {
                        positionHistory.Add(location);
                    }
                }
            }
            // return the final position
            return position;
        }
    }
}
