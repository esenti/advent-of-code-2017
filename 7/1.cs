using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aoc
{
    class Day7
    {
        static void Main()
        {
            // fwft (72) -> ktlj, cntj, xhth
            string lineRegex = @"(\w+) \((\d+)\)(?: -> (.+))?";

            Regex regex = new Regex(lineRegex);

            // Dictionary<string, int> nodes = new Dictionary<string, int>();
            HashSet<string> nodes = new HashSet<string>();
            HashSet<string> nodesWithParents = new HashSet<string>();

            foreach(string line in File.ReadLines("input.txt")) {
                Console.WriteLine(line);
                var match = regex.Match(line);

                Console.WriteLine(match.Groups[1]);
                Console.WriteLine(match.Groups[2]);
                if(match.Groups[3].Success) {
                    string[] children = Regex.Split(match.Groups[3].Value, ", ");
                    Console.WriteLine(string.Join(":", children));
                    nodes.Add(match.Groups[1].Value);

                    foreach(string child in children) {
                        nodesWithParents.Add(child);
                    }
                }
            }

            nodes.ExceptWith(nodesWithParents);

            foreach(string n in nodes) {
                Console.WriteLine(n);
            }


            // Console.WriteLine(step);
        }
    }
}
