using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aoc
{
    class Day14
    {
        static string KnotHash(string input) {

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

            return string.Join("", blocks.Select(i => i.ToString("x2")));
        }

        static bool MakeRegions(int region, int x, int y, bool[,] squares, int[,] regions) {
            if(!squares[x, y] || regions[x, y] != 0) {
                return false;
            }

            regions[x, y] = region;

            if(x - 1 >= 0) {
                MakeRegions(region, x - 1, y, squares, regions);
            }
            if(x + 1 < squares.GetLength(0)) {
                MakeRegions(region, x + 1, y, squares, regions);
            }
            if(y - 1 >= 0) {
                MakeRegions(region, x, y - 1, squares, regions);
            }
            if(y + 1 < squares.GetLength(1)) {
                MakeRegions(region, x, y + 1, squares, regions);
            }

            return true;
        }

        static void Main()
        {
            string key = File.ReadAllText("input.txt").Trim();
            bool[,] squares = new bool[128, 128];
            int[,] regions = new int[128, 128];

            for(int i = 0; i < 128; i++) {
                Console.WriteLine($"{key}-{i}");
                string hash = KnotHash($"{key}-{i}");

                int column = 0;
                foreach(char digit in hash) {
                    uint bits = uint.Parse(digit.ToString(), System.Globalization.NumberStyles.HexNumber);

                    for(int j = 3; j >= 0; j--) {
                        bool used = (bits & (1 << j)) != 0;

                        squares[i, column + (3 - j)] = used;
                    }

                    column += 4;
                }

                Console.WriteLine(hash);
            }

            int region = 1;

            for(int x = 0; x < 128; x++) {
                for(int y = 0; y < 128; y++) {
                    bool created = MakeRegions(region, x, y, squares, regions);

                    region += created ? 1 : 0;
                }
            }

            for(int x = 0; x < 128; x++) {
                for(int y = 0; y < 128; y++) {
                    Console.Write(regions[x, y]);
                }

                Console.WriteLine("");
            }

            Console.WriteLine(region - 1);
        }
    }
}
