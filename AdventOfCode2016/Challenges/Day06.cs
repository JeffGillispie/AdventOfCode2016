using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Challenges
{
    class Day06 : Day
    {
        public string Part1(string input)
        {
            string[] recordedMessages = System.IO.File.ReadAllLines(input);
            var messageCharacters = recordedMessages
                .First()                                                                                // get the first message
                .Select((letter, index) => recordedMessages                                             // iterate through the characters of the first mesasge and grap the index
                    .GroupBy(msg => msg[index])                                                         // group the characters by the column index
                    .ToDictionary(groupedChar => groupedChar.Key, groupedChar => groupedChar.Count()))  // cast as a set of maps of char values and instance counts
                .Select(frequencyMap => frequencyMap                                                    // iterate over the set of maps
                    .OrderByDescending(col => col.Value)                                                // order the map in descending instance count order
                    .First()                                                                            // get the most frequent instance
                    .Key);                                                                              // return the character
            string message = String.Join(String.Empty, messageCharacters);                              // join the message characters to a string
            return message;
        }

        public string Part2(string input)
        {
            string[] recordedMessages = System.IO.File.ReadAllLines(input);
            var messageCharacters = recordedMessages
                .First()                                                                                // get the first message
                .Select((letter, index) => recordedMessages                                             // iterate through the characters of the first mesasge and grap the index
                    .GroupBy(msg => msg[index])                                                         // group the characters by the column index
                    .ToDictionary(groupedChar => groupedChar.Key, groupedChar => groupedChar.Count()))  // cast as a set of maps of char values and instance counts
                .Select(frequencyMap => frequencyMap                                                    // iterate over the set of maps
                    .OrderBy(col => col.Value)                                                          // order the map in instance count order
                    .First()                                                                            // get the least frequent instance
                    .Key);                                                                              // return the character
            string message = String.Join(String.Empty, messageCharacters);                              // join the message characters to a string
            return message;
        }
    }
}
