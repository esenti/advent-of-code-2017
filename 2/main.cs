using System;
using System.IO;
using System.Linq;

namespace Aoc
{
    class Day2
    {
        static void Main()
        {

            int checksum = 0;

            foreach(string line in File.ReadLines("input.txt")) {
                Console.WriteLine(line);
                int[] numbers = Array.ConvertAll(line.Split('\t'), int.Parse);

                checksum += numbers.Max() - numbers.Min();
            }

            Console.WriteLine(checksum);
        }
    }
}
