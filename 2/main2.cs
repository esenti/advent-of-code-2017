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

                for(int i = 0; i < numbers.Length; i++) {
                    for(int j = 0; j < numbers.Length; j++) {
                        if(i != j) {
                            if(numbers[i] % numbers[j] == 0) {
                                checksum += numbers[i] / numbers[j];
                                break;
                            }
                        }
                    }
                }
            }

            Console.WriteLine(checksum);
        }
    }
}
