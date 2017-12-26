using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aoc
{
    class Day23
    {
        static bool IsPrime(long number)
        {
            for(int i = 2; i <= Math.Ceiling(Math.Sqrt(number)); ++i)
            {
                if(number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        static void Main()
        {
            long b = 107900;
            long c = 124900;

            long h = 0;

            while(true)
            {
                if(!IsPrime(b))
                {
                    ++h;
                }

                if(b == c)
                {
                    break;
                }

                b += 17;
            }

            Console.WriteLine(h);
        }
    }
}

