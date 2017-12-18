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

            int garbageCount = 0;
            bool garbage = false;

            for(int i = 0; i < stream.Length; i++) {
                Console.WriteLine(stream[i]);

                switch(stream[i]) {
                    case '<':
                        if(garbage) {
                            garbageCount++;
                        }
                        garbage = true;
                        break;
                    case '>':
                        garbage = false;
                        break;
                    case '!':
                        i++;
                        break;
                    default:
                        if(garbage) {
                            garbageCount++;
                        }
                        break;
                }
            }

            Console.WriteLine(garbageCount);
        }
    }
}
