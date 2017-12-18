using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    class Day13
    {
        static int CalculatePosition(int range, int depth) {
            int n = 2 * range - 2;

            int mod = depth % n;

            if(mod < range) {
                return mod;
            } else {
                return range - 2 - mod % range;
            }
        }

        static void Main()
        {
            Dictionary<int, int> layers = new Dictionary<int, int>();
            int severity = 0;

            foreach(string line in File.ReadLines("input.txt")) {
                Console.WriteLine(line);

                int layer = int.Parse(line.Split(':')[0]);
                int range = int.Parse(line.Split(':')[1]);

                layers[layer] = range;
            }

            int lastLayer = layers.Keys.Max();

            for(int depth = 0; depth <= lastLayer; depth++) {
                Console.WriteLine("I'm in layer {0}", depth);
                if(layers.ContainsKey(depth)) {
                    int position = CalculatePosition(layers[depth], depth);

                    Console.WriteLine("Firewall is at {0}", position);

                    if(position == 0) {
                        Console.WriteLine("I've got caught!");
                        severity += depth * layers[depth];
                    }
                }
            }

            Console.WriteLine("---");
            Console.WriteLine(severity);
        }
    }
}
