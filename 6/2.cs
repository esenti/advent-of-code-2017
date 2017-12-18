using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    class Day6
    {
        static void BalanceBank(int[] banks, int index) {
            int v = banks[index];
            banks[index] = 0;

            while(v-- > 0) {
                index++;

                banks[index % banks.Length]++;
            }

        }

        static void Main()
        {

            string content = File.ReadAllText("input.txt");
            Console.WriteLine(content);
            int[] banks = Array.ConvertAll(content.Split('\t'), int.Parse);

            List<int> seenValues = new List<int>();

            int step = 0;
            int distance = 0;

            while(step++ > -1) {
                int index = Array.IndexOf(banks, banks.Max());
                BalanceBank(banks, index);

                int hash = string.Join(", ", banks).GetHashCode();

                if(seenValues.Contains(hash)) {
                    distance = step - 1 - seenValues.IndexOf(hash);
                    break;
                }

                seenValues.Add(hash);

                Console.WriteLine(string.Join(", ", banks));
            }

            Console.WriteLine(distance);
        }
    }
}
