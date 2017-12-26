using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    class Day24
    {
        static void BuildBridge(int port, IEnumerable<Tuple<int, int>> components, ref int maxStrength, int strength=0)
        {
            if(strength > maxStrength)
            {
                maxStrength = strength;
            }

            foreach(Tuple<int, int> component in components)
            {
                if(component.Item1 == port)
                {
                    BuildBridge(component.Item2, components.Except(new [] {component}), ref maxStrength, strength + component.Item1 + component.Item2);
                }
                else if(component.Item2 == port)
                {
                    BuildBridge(component.Item1, components.Except(new [] {component}), ref maxStrength, strength + component.Item1 + component.Item2);
                }
            }
        }

        static void Main()
        {
            var components = new List<Tuple<int, int>>();

            foreach(string line in File.ReadLines("input.txt"))
            {
                int[] ports = Array.ConvertAll(line.Split('/'), int.Parse);

                components.Add(Tuple.Create(ports[0], ports[1]));
            }

            int maxStrength = 0;

            BuildBridge(0, components, ref maxStrength);
            Console.WriteLine(maxStrength);
        }
    }
}
