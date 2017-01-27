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
                .Sum(room => int.Parse(room.Split('-').Last().Split('[').First()));
            return roomSum.ToString();
        }

        public string Part2(string input)
        {
            string[] rooms = System.IO.File.ReadAllLines(input);
            // get a map of decrypted room names and sector ids
            var roomMap = rooms.Where(room => isRealRoom(room)).ToDictionary(room => decryptName(room), room => room.Split('-').Last().Split('[').First());
            // find the first first room that contains the word north
            var targetRoom = roomMap.Where(room => room.Key.Contains("north")).First();
            // return the sector id of the room where north pole objects are stored
            return targetRoom.Value;
        }

        private bool isRealRoom(string value)
        {
            // get the letters used in the encrypted name
            string encryptedLetters = String.Join(
                String.Empty, 
                value
                    .Split('-')
                    .Take(value.Count(character => character.Equals('-')))
                    .ToArray()
                );
            // parse the checksum
            string checksumKey = value
                .Substring(value.IndexOf('['))
                .Trim(new char[] { '[', ']' });
            // calculate the checksum from the encrypted letters
            var checksumCalculated = String.Join(
                String.Empty, 
                encryptedLetters
                    .GroupBy(letter => letter)
                    .ToDictionary(letter => letter.Key, letter => letter.Count())
                    .OrderByDescending(kvp => kvp.Value)    // order by letter count
                    .ThenBy(kvp => kvp.Key)                 // then order by letter value
                    .Select(kvp => kvp.Key)
                ).Substring(0, 5);                          // the checksum is 5 characters
            // if equals it is a real room
            return checksumKey.Equals(checksumCalculated);
        }

        private string decryptName(string room)
        {
            // parse the sector id value
            int sectorID = int.Parse(room.Split('-').Last().Split('[').First());
            // parse the encrypted room name
            string encryptedName = room.Substring(0, room.LastIndexOf('-'));
            // get the decrypted room name 
            string name = String.Join(                      // join each decrypted character
                String.Empty, 
                encryptedName
                    .Select(character =>                    // iterate through each character
                        (character.Equals('-'))             // check if the character is a dash
                            ? ' '                           // replace dash with space
                            : shift(character, sectorID)    // shift other characters
                        )
                );
            // return the decrypted room name
            return name;
        }

        private char shift(char input, int sectorID)
        {
            // shift the encrypted character 
            // by the modulos of the sector id
            // to account for when the value wraps
            return (char)(((int)input - (int)'a' + sectorID) % 26 + (int)'a');
        }
    }
}
