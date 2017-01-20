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

        private Dictionary<int, Day> challengeSelector = new Dictionary<int, Day>()
        {
            {  1, new Challenges.Day01() },
            {  2, new Challenges.Day02() },
            {  3, new Challenges.Day03() },
            {  4, new Challenges.Day04() },
            {  5, new Challenges.Day05() },
            {  6, new Challenges.Day06() },
            {  7, new Challenges.Day07() },
            {  8, new Challenges.Day08() },
            {  9, new Challenges.Day09() },
            { 10, new Challenges.Day10() },
            { 11, new Challenges.Day11() },
            { 12, new Challenges.Day12() },
            { 13, new Challenges.Day13() },
            { 14, new Challenges.Day14() },
            { 15, new Challenges.Day15() },
            { 16, new Challenges.Day16() },
            { 17, new Challenges.Day17() },
            { 18, new Challenges.Day18() },
            { 19, new Challenges.Day19() },
            { 20, new Challenges.Day20() },
            { 21, new Challenges.Day21() },
            { 22, new Challenges.Day22() },
            { 23, new Challenges.Day23() },
            { 24, new Challenges.Day24() },
            { 25, new Challenges.Day25() }
        };

        public Challenge(FileInfo instructions, FileInfo input, int dayOrdinal)
        {
            this.instructions = instructions;
            this.input = input;
            this.day = challengeSelector[dayOrdinal];            
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
