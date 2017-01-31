using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Challenges
{
    class Day08 : Day
    {
        public string Part1(string input)
        {
            string[] commands = System.IO.File.ReadAllLines(input);
            byte width = 50;
            byte height = 6;
            byte[,] screen = new byte[width, height];

            foreach (string command in commands)
            {
                execute(command, screen, height, width);
            }

            return getPixels(screen);
        }

        public string Part2(string input)
        {
            string[] commands = System.IO.File.ReadAllLines(input);
            byte width = 50;
            byte height = 6;
            byte[,] screen = new byte[width, height];

            foreach (string command in commands)
            {
                execute(command, screen, height, width);
            }

            string output = drawScreen(screen, width, height);
            return output;
        }

        private byte[,] drawRect(byte[,] screen, byte x, byte y)
        {
            for (int v = 0; v < y; v++)
            {
                for (int h = 0; h < x; h++)
                {
                    screen[h, v] = 1;
                }
            }

            return screen;
        }

        private byte[,] rotateX(byte[,] screen, byte size, byte pos, byte distance)
        {
            byte[] oldCol = new byte[size];
            byte[] newCol = new byte[size];

            for (byte y = 0; y < size; y++)
                oldCol[y] = screen[pos, y];

            for (byte y = 0; y < size; y++)
                newCol[(y + distance) % newCol.Length] = oldCol[y];

            for (byte y = 0; y < size; y++)
                screen[pos, y] = newCol[y];

            return screen;
        }

        private byte[,] rotateY(byte[,] screen, byte size, byte pos, byte distance)
        {
            byte[] oldRow = new byte[size];
            byte[] newRow = new byte[size];

            for (byte x = 0; x < size; x++)
                oldRow[x] = screen[x, pos];

            for (byte x = 0; x < size; x++)
                newRow[(x + distance) % newRow.Length] = oldRow[x];

            for (byte x = 0; x < size; x++)
                screen[x, pos] = newRow[x];

            return screen;
        }

        private byte[,] execute(string command, byte[,] screen, byte height, byte width)
        {
            string[] cmd = command.Split(' ');

            if (cmd[0].Equals("rect"))
            {
                string[] size = cmd[1].Split('x');
                byte x = byte.Parse(size[0]);
                byte y = byte.Parse(size[1]);
                return drawRect(screen, x, y);
            }
            else
            {
                byte pos = byte.Parse(cmd[2].Split('=')[1]);
                byte distance = byte.Parse(cmd[4]);

                if (cmd[1].Equals("row"))
                {
                    return rotateY(screen, width, pos, distance);
                }
                else
                {
                    return rotateX(screen, height, pos, distance);
                }
            }
        }

        private string getPixels(byte[,] screen)
        {
            int count = 0;

            foreach (byte b in screen)
            {
                if (b == 1)
                {
                    count++;
                }
            }

            return count.ToString();
        }

        private string drawScreen(byte[,] screen, byte x, byte y)
        {
            StringBuilder sb = new StringBuilder();

            for (byte v = 0; v < y; v++)
            {
                for (byte h = 0; h < x; h++)
                {
                    byte pixel = screen[h, v];
                    string pix = (pixel == 0) ? " " : "0";
                    sb.Append(pix);
                }

                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
