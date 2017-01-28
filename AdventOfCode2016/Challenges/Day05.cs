using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Challenges
{
    class Day05 : Day
    {
        public string Part1(string input)
        {
            // setup
            string doorID = System.IO.File.ReadAllText(input);
            StringBuilder password = new StringBuilder();
            int i = 0;
            // the password is 8 characters
            while (password.Length < 8)
            {
                string hex = (doorID + i.ToString()).GetHash();                
                // the hex value should start with 5 zeros
                while (!hex.StartsWith("00000")) 
                {
                    i++;
                    hex = (doorID + i.ToString()).GetHash();                    
                }
                // character found add to password
                password.Append(hex[5]);
                i++;
            }
            // return password
            return password.ToString();
        }

        public string Part2(string input)
        {
            // setup
            string doorID = System.IO.File.ReadAllText(input);            
            string[] password = new string[8];
            int i = 0;
            int charsFound = 0;
            // the password is 8 characters
            while (charsFound < 8)
            {
                string hex = (doorID + i.ToString()).GetHash();
                // the hex value should start with 5 zeros
                while (!hex.StartsWith("00000"))
                {
                    i++;
                    hex = (doorID + i.ToString()).GetHash();
                }
                // potential character found check the position value
                int pos = 8; // set position to an invalid value
                bool isParsed = int.TryParse(hex[5].ToString(), out pos);
                // validate position is valid and
                // validate it is the first result for that position
                if (pos < 8 && String.IsNullOrEmpty(password[pos]) && isParsed)
                {
                    password[pos] = hex[6].ToString();
                    charsFound++;
                }
                i++;
            }
            // return the password
            return String.Join(String.Empty, password);            
        }
    }
}
