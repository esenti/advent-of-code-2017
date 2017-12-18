using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    class Day4
    {
        static bool IsValid(string passphrase) {
            List<string> words = new List<string>();

            foreach(string word in passphrase.Split(' ')) {
                string sortedWord = String.Concat(word.OrderBy(c => c));

                if(words.Contains(sortedWord)) {
                    return false;
                } else {
                    words.Add(sortedWord);
                }
            }

            return true;
        }

        static void Main()
        {

            int numValid = 0;

            foreach(string line in File.ReadLines("input.txt")) {
                Console.WriteLine(line);

                if(IsValid(line)) {
                    numValid++;
                }
            }

            Console.WriteLine(numValid);
        }
    }
}
