using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Challenge
    {
        private FileInfo instructions;
        private FileInfo input;
        private Day day;

        public Challenge(FileInfo instructions, FileInfo input, int dayOrdinal)
        {
            this.instructions = instructions;
            this.input = input;
            this.day = null;

            switch(dayOrdinal)
            {
                case 1:
                    this.day = new Challenges.Day01();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 11:
                    break;
                case 12:
                    break;
                case 13:
                    break;
                case 14:
                    break;
                case 15:
                    break;
                case 16:
                    break;
                case 17:
                    break;
                case 18:
                    break;
                case 19:
                    break;
                case 20:
                    break;
                case 21:
                    break;
                case 22:
                    break;
                case 23:
                    break;
                case 24:
                    break;
                case 25:
                    break;
                default:
                    break;
            }
        }

        public FileInfo Instructions
        {
            get
            {
                return this.instructions;
            }
        }

        public FileInfo Input
        {
            get
            {
                return this.input;
            }
        }

        public string GetPart1Answer()
        {
            if (this.day != null)
            {
                return this.day.Part1(this.input.FullName);
            }
            else
            {
                return String.Empty;
            }
        }

        public string GetPart2Answer()
        {
            if (this.day != null)
            {
                return this.day.Part2(this.input.FullName);
            }
            else
            {
                return String.Empty;
            }
        }
    }
}
