using System;
namespace Aoc
{
    class Day1
    {
        static void Main()
        {
            String s = Console.ReadLine();

            int sum = 0;

            for(int i = 0; i < s.Length; i++) {
                int currentIndex = i;
                int nextIndex = (i + s.Length / 2) % s.Length;

                if(s[currentIndex] == s[nextIndex]) {
                    sum += (int)Char.GetNumericValue(s[currentIndex]);
                }
            }

            Console.WriteLine(sum);
        }
    }
}
