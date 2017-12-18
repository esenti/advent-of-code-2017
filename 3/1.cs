using System;

namespace Aoc
{
    class Day3
    {
        static void Main()
        {
            String s = Console.ReadLine();

            int num = int.Parse(s);

            int pow = 0;

            for(int i = 1;;i += 2) {
                if(i * i >= num) {
                    pow = i;
                    break;
                }
            }

            num = num - (pow - 2) * (pow - 2);

            int ffs = pow - 1;

            int[] dist = new int[pow - 1];

            for(int i = 0; i < pow - 1;  i++) {
                if(i >= pow / 2) {
                    dist[i] = ffs++;
                } else {
                    dist[i] = ffs--;
                }
            }

            int distIndex = num % (pow - 1);
            int distance = dist[distIndex];

            Console.WriteLine(distance);
        }
    }
}

