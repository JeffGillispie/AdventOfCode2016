using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Challenges
{
    class Day03 : Day
    {
        public string Part1(string input)
        {
            string[] triangles = System.IO.File.ReadAllLines(input);
            int validTriangles = triangles.Count(t => isTriangle(parseTriangleSides(t)));
            return validTriangles.ToString();
        }

        public string Part2(string input)
        {
            string[] lines = System.IO.File.ReadAllLines(input);
            int validTriangles = 0;
            // get count of possible triangles with side values in groups of three vertically
            // iterate through every third line or every triangle
            for (int i = 0; i < lines.Length; i += 3)
            {
                int[,] triangles = new int[3, 3]; // set of three triangles
                // parse the side values of each of the three triangles 
                for (int j = 0; j < 3; j++)
                {
                    Tuple<int, int, int> sideSet = parseTriangleSides(lines[i + j]);
                    triangles[j, 0] = sideSet.Item1;
                    triangles[j, 1] = sideSet.Item2;
                    triangles[j, 2] = sideSet.Item3;
                }
                // now loop over each triangle
                for (int j = 0; j < 3; j++)
                {
                    Tuple<int, int, int> sides = new Tuple<int, int, int>(triangles[0, j], triangles[1, j], triangles[2, j]);
                    // check if the sides are a valid triangle
                    if (isTriangle(sides))
                    {
                        validTriangles++;
                    }
                }
            }

            return validTriangles.ToString();
        }

        private bool isTriangle(Tuple<int, int, int> sides)
        {
            return (sides.Item1 + sides.Item2 > sides.Item3 &&
                    sides.Item1 + sides.Item3 > sides.Item2 &&
                    sides.Item2 + sides.Item3 > sides.Item1);
        }

        private Tuple<int, int, int> parseTriangleSides(string triangle)
        {
            string[] sides = triangle.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            // parse the side lengths
            int x = int.Parse(sides[0]);
            int y = int.Parse(sides[1]);
            int z = int.Parse(sides[2]);
            return new Tuple<int, int, int>(x, y, z);
        }
    }
}
