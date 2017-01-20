using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Challenges
{
    class Day02 : Day
    {
        public string Part1(string input)
        {
            string[] instructions = System.IO.File.ReadAllLines(input);
            string[,] keypad =
            {
                { "1", "2", "3" },
                { "4", "5", "6" },
                { "7", "8", "9" }
            };
            CartesianCoordinate initalPosition = new CartesianCoordinate(1, 1);
            string doorCode = processInstructions(instructions, keypad, initalPosition);
            return doorCode;
        }

        public string Part2(string input)
        {
            string[] instructions = System.IO.File.ReadAllLines(input);            
            string[,] keypad =
            {
                { null, null, "1", null, null },
                { null,  "2", "3",  "4", null },
                {  "5",  "6", "7",  "8",  "9" },
                { null,  "A", "B",  "C", null },
                { null, null, "D", null, null }
            };
            CartesianCoordinate initalPosition = new CartesianCoordinate(2, 0);
            string doorCode = processInstructions(instructions, keypad, initalPosition);
            return doorCode;
        }
        
        private string processInstructions(string[] instructions, string[,] keypad, CartesianCoordinate initalPosition)
        {
            StringBuilder doorCode = new StringBuilder();
            CartesianCoordinate coords = initalPosition;
            // loop over instructions
            foreach (string line in instructions)
            {
                // loop over each move in the line
                foreach (char instruction in line)
                {
                    // parse the direction                    
                    Direction.Relative2D direction = (Direction.Relative2D)Enum.Parse(typeof(Direction.Relative2D), instruction.ToString());
                    coords.Move(direction, keypad);
                }
                // append the next value of the door code
                string digit = keypad[coords.Y, coords.X];
                doorCode.Append(digit);
            }
            // get door code
            return doorCode.ToString();
        }                
    }
}
