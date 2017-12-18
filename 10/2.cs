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
            string input = File.ReadAllText("input.txt").Trim();

            int[] lengthsSuffix = { 17, 31, 73, 47, 23 };
            int[] lengths = input.Select(i => (int)i).ToArray();


            List<int> list = new List<int>();
            list.AddRange(lengths);
            list.AddRange(lengthsSuffix);

            lengths = list.ToArray();

            Console.WriteLine(string.Join(", ", lengths));

            Stack<int> stack = new Stack<int>();
            int current = 0;
            int skip = 0;

            int[] array = Enumerable.Range(0, 256).ToArray();

            for(int r = 0; r < 64; r++) {
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
            }

            List<int> blocks = new List<int>();

            for(int i = 0; i < 16; i++) {
                int block = array[i * 16];

                for(int j = 1; j < 16; j++) {
                    block ^= array[i * 16 + j];
                }

                blocks.Add(block);
            }

            Console.WriteLine(string.Join("", blocks.Select(i => i.ToString("x2"))));
        }
    }
}
