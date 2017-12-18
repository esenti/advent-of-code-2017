using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aoc
{
    class Day9
    {
        static void Main()
        {
            string stream = File.ReadAllText("input.txt");

            int score = 0;
            bool garbage = false;
            int group = 0;

            for(int i = 0; i < stream.Length; i++) {
                Console.WriteLine(stream[i]);
                switch(stream[i]) {
                    case '{':
                        if(!garbage) {
                            group++;
                        }
                        break;
                    case '}':
                        if(!garbage) {
                            score += group;
                            group--;
                        }
                        break;
                    case '<':
                        garbage = true;
                        break;
                    case '>':
                        garbage = false;
                        break;
                    case '!':
                        i++;
                        break;
                }
            }

            Console.WriteLine(score);
        }
    }
}
