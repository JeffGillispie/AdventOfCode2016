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
                    this.day = new Challenges.Day02();
                    break;
                case 3:
                    this.day = new Challenges.Day03();
                    break;
                case 4:
                    this.day = new Challenges.Day04();
                    break;
                case 5:
                    this.day = new Challenges.Day05();
                    break;
                case 6:
                    this.day = new Challenges.Day06();
                    break;
                case 7:
                    this.day = new Challenges.Day07();
                    break;
                case 8:
                    this.day = new Challenges.Day08();
                    break;
                case 9:
                    this.day = new Challenges.Day09();
                    break;
                case 10:
                    this.day = new Challenges.Day10();
                    break;
                case 11:
                    this.day = new Challenges.Day11();
                    break;
                case 12:
                    this.day = new Challenges.Day12();
                    break;
                case 13:
                    this.day = new Challenges.Day13();
                    break;
                case 14:
                    this.day = new Challenges.Day14();
                    break;
                case 15:
                    this.day = new Challenges.Day15();
                    break;
                case 16:
                    this.day = new Challenges.Day16();
                    break;
                case 17:
                    this.day = new Challenges.Day17();
                    break;
                case 18:
                    this.day = new Challenges.Day18();
                    break;
                case 19:
                    this.day = new Challenges.Day19();
                    break;
                case 20:
                    this.day = new Challenges.Day20();
                    break;
                case 21:
                    this.day = new Challenges.Day21();
                    break;
                case 22:
                    this.day = new Challenges.Day22();
                    break;
                case 23:
                    this.day = new Challenges.Day23();
                    break;
                case 24:
                    this.day = new Challenges.Day24();
                    break;
                case 25:
                    this.day = new Challenges.Day25();
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
