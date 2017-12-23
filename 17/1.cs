using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aoc
{
    class Day17
    {
        static void Main()
        {
            int input = int.Parse(File.ReadAllText("input.txt"));

            List<int> buffer = new List<int>{ 0 };

            int position = 0;
            for(int i = 1; i <= 2017; ++i) {
                position = (position + input) % buffer.Count;

                buffer.Insert(++position, i);
            }

            int index = buffer.IndexOf(2017);
            Console.WriteLine(index);
            Console.WriteLine(buffer[(index + 1) % buffer.Count]);
        }
    }
}
