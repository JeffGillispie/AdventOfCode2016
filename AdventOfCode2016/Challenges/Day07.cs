using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2016.Challenges
{
    class Day07 : Day
    {
        private const string ABBA_PATTERN = @"([a-zA-Z])((?!\1)[a-zA-Z])\2\1";
        private const string ABA_PATTERN = @"([a-zA-Z])(?:(?!\1)[a-zA-Z])\1";

        public string Part1(string input)
        {
            string[] addresses = System.IO.File.ReadAllLines(input);
            var TLSaddresses = addresses.Where(address => supportsTLS(address));
            return TLSaddresses.Count().ToString();
        }

        public string Part2(string input)
        {
            string[] addresses = System.IO.File.ReadAllLines(input);
            var SSLaddresses = addresses.Where(address => supportsSSL(address));
            return SSLaddresses.Count().ToString();
        }

        private bool supportsTLS(string value)
        {
            // split into hypernet and supernet sequences
            NestedString nested = new NestedString(value, '[', ']');
            List<string> hypernetSequences = nested.InsideSequences;
            List<string> supernetSequences = nested.OutsideSequences;
            // now validate the sequences
            string hypernet = String.Join("-", hypernetSequences);
            string supernet = String.Join("-", supernetSequences);
            bool isHypernetValid = !containsABBA(hypernet);
            bool isSupernetValid = containsABBA(supernet);
            // both need to be valid to support TLS
            return isHypernetValid && isSupernetValid;
        }

        private bool containsABBA(string value)
        {
            Regex regex = new Regex(ABBA_PATTERN);
            Match match = regex.Match(value);
            return match.Success;
        }

        private bool supportsSSL(string value)
        {
            // split into hypernet and supernet sequences
            NestedString nested = new NestedString(value, '[', ']');
            List<string> hypernetSequences = nested.InsideSequences;
            List<string> supernetSequences = nested.OutsideSequences;
            // validate the sequences
            string hypernet = String.Join("-", hypernetSequences);
            string supernet = String.Join("-", supernetSequences);
            Regex aba = new Regex(ABA_PATTERN);
            Match abaMatch = aba.Match(supernet);
            // check all possible aba supernet matches
            while(abaMatch.Success)
            {
                string a = abaMatch.Value[0].ToString();
                string b = abaMatch.Value[1].ToString();                
                Regex bab = new Regex(b + a + b);
                Match babMatch = bab.Match(hypernet);
                // check for the corresponding bab hypernet match
                if (babMatch.Success)
                {
                    // match found value is SSL
                    return true;
                }
                // advance to the next match
                abaMatch = aba.Match(supernet, abaMatch.Index + 1);                
            }
            // no match found value is not SSL
            return false;
        }
    }
}
