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
            int validTriangles = 0;
            // get count of possible triangles
            foreach (string triangle in triangles)
            {
                string[] sides = triangle.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                // parse the side lengths
                int x = int.Parse(sides[0]);
                int y = int.Parse(sides[1]);
                int z = int.Parse(sides[2]);
                // check if these are the sides of a valid triangle
                if (x + y > z &&
                    x + z > y &&
                    y + z > x)
                {
                    validTriangles++;
                }
            }

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
                int[,] triangles = new int[3, 3];
                // parse the side values of each of the three triangles 
                for (int j = 0; j < 3; j++)
                {
                    string[] parts = lines[i + j].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    triangles[j, 0] = int.Parse(parts[0]);
                    triangles[j, 1] = int.Parse(parts[1]);
                    triangles[j, 2] = int.Parse(parts[2]);
                }
                // now loop over each triangle
                for (int j = 0; j < 3; j++)
                {
                    int x = triangles[0, j];
                    int y = triangles[1, j];
                    int z = triangles[2, j];
                    // check if the sides are a valid triangle
                    if (x + y > z &&
                    x + z > y &&
                    y + z > x)
                    {
                        validTriangles++;
                    }
                }
            }

            return validTriangles.ToString();
        }
    }
}
