using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aoc
{
    class Day10
    {
        static void Main()
        {
            int[] lengths = Array.ConvertAll(File.ReadAllText("input.txt").Split(','), int.Parse);

            Stack<int> stack = new Stack<int>();
            int current = 0;
            int skip = 0;

            int[] array = Enumerable.Range(0, 256).ToArray();

            foreach(int length in lengths) {

                for(int i = current; i < current + length; i++) {
                    int index = i % array.Length;

                    stack.Push(array[index]);
                }

                for(int i = current; i < current + length; i++) {
                    int index = i % array.Length;

                    array[index] = stack.Pop();
                }

                current = (current + length + skip++) % array.Length;
            }

            Console.WriteLine(string.Join(", ", array));
            Console.WriteLine(array[0] * array[1]);
        }
    }
}
