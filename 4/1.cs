using System;
using System.IO;
using System.Collections.Generic;

namespace Aoc
{
    class Day4
    {
        static bool IsValid(string passphrase) {
            List<string> words = new List<string>();

            foreach(string word in passphrase.Split(' ')) {
                if(words.Contains(word)) {
                    return false;
                } else {
                    words.Add(word);
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
