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
                int direction = (instruction.Substring(0,1).Equals("R")) ? 1 : -1;
                int distance = int.Parse(instruction.Substring(1));
                bearing.Move(direction, distance);
            }

            return bearing.Distance.ToString();
        }

        public string Part2(string input)
        {
            string[] instructions = System.IO.File.ReadAllText(input).Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            Bearing bearing = new Bearing();

            foreach (string instruction in instructions)
            {
                int direction = (instruction.Substring(0, 1).Equals("R")) ? 1 : -1;
                int distance = int.Parse(instruction.Substring(1));
                bool isFinished = bearing.MoveAndLog(direction, distance);

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
                    return Math.Abs(this.position.x) + Math.Abs(this.position.y);
                }
            }

            public void Move(int direction, int distance)
            {
                if ((int)this.direction == 0 && direction == -1)
                {
                    this.direction = Direction.west;
                }
                else if ((int)this.direction == 3 && direction == 1)
                {
                    this.direction = Direction.north;
                }
                else
                {
                    this.direction += direction;
                }

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
                    ? Direction.west 
                    : ((int)this.direction == 3 && direction == 1) 
                        ? Direction.north 
                        : this.direction + direction;

                int count = 0;
                int startValue = 0;
                int endValue = 0;
                bool isIncrement = true;
                bool isHorizontal = true;
                bool isFinished = false;

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

                startValue = (isHorizontal) ? this.position.x : this.position.y;

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
                string location = String.Format("{0},{1}", this.position.x, this.position.y);

                if (this.positionHistory.Contains(location))
                {
                    return true;
                }
                else
                {
                    this.positionHistory.Add(location);
                    return false;
                }
            }
        }
    }
}
