using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Challenges
{
    class Day01 : Day
    {
        public string Part1(string input)
        {
            string[] instructions = System.IO.File.ReadAllText(input).Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            Bearing bearing = new Bearing();

            foreach (string instruction in instructions)
            {
                // parse the direction and the distance from the instruction
                int direction = (instruction.Substring(0,1).Equals("R")) ? 1 : -1;
                int distance = int.Parse(instruction.Substring(1));
                // move to the new location specified in the instruction
                bearing.Move(direction, distance);
            }

            // return the distance traveled
            return bearing.Distance.ToString();
        }

        public string Part2(string input)
        {
            string[] instructions = System.IO.File.ReadAllText(input).Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            Bearing bearing = new Bearing();

            foreach (string instruction in instructions)
            {
                // parse the direction and the distance from the instruction
                int direction = (instruction.Substring(0, 1).Equals("R")) ? 1 : -1;
                int distance = int.Parse(instruction.Substring(1));
                // move to the new location spevified in the instruction
                bool isFinished = bearing.MoveAndLog(direction, distance);
                // check if we have passed over a location that has been previously visited
                if (isFinished)
                {
                    break;
                }
            }

            return bearing.Distance.ToString();
        }

        public enum Direction
        {
            north = 0,
            east = 1,
            south = 2,
            west = 3
        }

        class Position
        {
            public int x;
            public int y;

            public Position(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        class Bearing
        {
            public Position position = new Position(0, 0);
            public Direction direction = Direction.north;
            private HashSet<string> positionHistory = new HashSet<string>();

            public int Distance
            {
                get
                {
                    // absolute value of the sum of the vertical and horizontal position
                    return Math.Abs(this.position.x) + Math.Abs(this.position.y);
                }
            }

            public void Move(int direction, int distance)
            {
                // find the new direction based on the existing direction
                if ((int)this.direction == 0 && direction == -1)
                {
                    // left turn from north to west
                    this.direction = Direction.west;
                }
                else if ((int)this.direction == 3 && direction == 1)
                {
                    // right turn from west to north
                    this.direction = Direction.north;
                }
                else
                {
                    // direction is in bounds so just add it
                    this.direction += direction;
                }

                // now that we have the new direction
                // move the specified distance
                switch(this.direction)
                {
                    case Direction.north:
                        this.position.y += distance;
                        break;
                    case Direction.east:
                        this.position.x += distance;
                        break;
                    case Direction.south:
                        this.position.y -= distance;
                        break;
                    case Direction.west:
                        this.position.x -= distance;
                        break;
                }
            }

            public bool MoveAndLog(int direction, int distance)
            {
                this.direction = ((int)this.direction == 0 && direction == -1) 
                    ? Direction.west // turn left from north to west
                    : ((int)this.direction == 3 && direction == 1) 
                        ? Direction.north // turn right from west to north
                        : this.direction + direction; // direction is in bounds

                // setup
                int count = 0;
                int startValue = 0;
                int endValue = 0;
                bool isIncrement = true;
                bool isHorizontal = true;
                bool isFinished = false;

                // based on the new direction
                // determine if the move is horizontal or vertical
                // and if the move will be in the positive or negative direction
                // then set the end value so we can loop through the intermediate positions
                switch (this.direction)
                {
                    case Direction.north:
                        isIncrement = true;
                        isHorizontal = false;
                        endValue = this.position.y + distance;
                        break;
                    case Direction.east:
                        isIncrement = true;
                        isHorizontal = true;
                        endValue = this.position.x + distance;
                        break;
                    case Direction.south:
                        isIncrement = false;
                        isHorizontal = false;
                        endValue = this.position.y - distance;
                        break;
                    case Direction.west:
                        isIncrement = false;
                        isHorizontal = true;
                        endValue = this.position.x - distance;
                        break;
                }
                // set the starting value
                startValue = (isHorizontal) ? this.position.x : this.position.y;
                // iterate through the intermediate positions
                // exit if we have arrived at a location we have already visited
                if (isIncrement)
                {
                    for (int i = startValue; i <= endValue; i++)
                    {
                        if (isHorizontal)
                            this.position.x = i;
                        else
                            this.position.y = i;

                        if (count > 0)
                        {
                            if (logPosition())
                            {
                                isFinished = true;
                                break;
                            }
                        }

                        count++;
                    }
                }
                else
                {
                    for (int i = startValue; i >= endValue; i--)
                    {
                        if (isHorizontal)
                            this.position.x = i;
                        else
                            this.position.y = i;

                        if (count > 0)
                        {
                            if (logPosition())
                            {
                                isFinished = true;
                                break;
                            }
                        }

                        count++;
                    }
                }

                return isFinished;
            }

            private bool logPosition()
            {
                // get the coordinates of our position
                string location = String.Format("{0},{1}", this.position.x, this.position.y);

                if (this.positionHistory.Contains(location))
                {
                    // arrived at our destination so its ok to exit
                    return true;
                }
                else
                {
                    // log the current position
                    // not finished
                    this.positionHistory.Add(location);
                    return false;
                }
            }
        }
    }
}
