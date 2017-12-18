using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    class Day5
    {
        static void Main()
        {

            List<int> offsets = new List<int>();

            foreach(string line in File.ReadLines("input.txt")) {
                offsets.Add(int.Parse(line));
            }

            int index = 0;
            int count = 0;

            while(index < offsets.Count) {
                int oldIndex = index;
                index += offsets[index];

                offsets[oldIndex] += offsets[oldIndex] >= 3 ? -1 : 1;
                count++;
            }

            Console.WriteLine(count);
        }
    }
}
