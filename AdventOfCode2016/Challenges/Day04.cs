using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Challenges
{
    class Day04 : Day
    {
        public string Part1(string input)
        {
            string[] rooms = System.IO.File.ReadAllLines(input);
            // sum the sector id of all real rooms
            int roomSum = rooms
                .Where(room => isRealRoom(room))
                .Sum(room => int.Parse(room.Split('-').Last().Split('[').First())); // abc-x-y-z-123[checksum]
            return roomSum.ToString();
        }

        public string Part2(string input)
        {
            string[] rooms = System.IO.File.ReadAllLines(input);
            // get a map of decrypted room names and sector ids
            var roomMap = rooms
                .Where(room => isRealRoom(room))
                .ToDictionary(
                    room => decryptName(room), 
                    room => room.Split('-').Last().Split('[').First() // abc-x-y-z-123[checksum]
                    );
            // find the first first room that contains the word north
            var targetRoom = roomMap
                .Where(room => room.Key.Contains("north"))
                .First();
            // return the sector id of the room where north pole objects are stored
            return targetRoom.Value;
        }

        private bool isRealRoom(string value)
        {
            // get the letters used in the encrypted name
            int segmentCount = value.Count(character => character.Equals('-')); // abc-x-y-z-123[checksum]
            var characters = value
                .Split('-')
                .Take(segmentCount);
            string encryptedLetters = String.Join(String.Empty, characters);
            // parse the checksum
            string checksumKey = value
                .Substring(value.IndexOf('['))
                .Trim(new char[] { '[', ']' });
            // calculate the checksum from the encrypted letters
            var sortedLetters = encryptedLetters
                .GroupBy(letter => letter)
                .ToDictionary(
                    letter => letter.Key, 
                    letter => letter.Count())
                .OrderByDescending(kvp => kvp.Value)    // order by letter count
                .ThenBy(kvp => kvp.Key)                 // then order by letter value
                .Select(kvp => kvp.Key);
            string checksumCalculated = String.Join(String.Empty, sortedLetters).Substring(0, 5);                
            // if equals it is a real room
            return checksumKey.Equals(checksumCalculated);
        }

        private string decryptName(string room)
        {
            // parse the sector id value
            int sectorID = int.Parse(room.Split('-').Last().Split('[').First()); // abc-x-y-z-123[checksum]
            // parse the encrypted room name
            string encryptedName = room.Substring(0, room.LastIndexOf('-'));
            // get the decrypted room name
            var decryptedCharacters = encryptedName
                .Select(character => (character.Equals('-'))    // if the character is a dash
                    ? ' '                                       // replace it with a space
                    : shift(character, sectorID)                // shift other characters
                    );
            string name = String.Join(String.Empty, decryptedCharacters);            
            // return the decrypted room name
            return name;
        }

        private char shift(char input, int sectorID)
        {
            // shift the encrypted character 
            // by the modulus of the sector id
            // to account for when the value wraps
            return (char)(((int)input - (int)'a' + sectorID) % 26 + (int)'a');
        }
    }
}
