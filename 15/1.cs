using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aoc
{
    class Generator {
        private uint current;
        private uint factor;

        public Generator(uint current_, uint factor_) {
            current = current_;
            factor = factor_;
        }

        public uint Next() {
            current = (uint)(((long)current * (long)factor) % 2147483647);
            return current;
        }
    }

    class Judge {
        public int Count { get; private set; }

        public Judge() {
        }

        public void Compare(uint a, uint b) {
            if((a & 0x0000ffff) == (b & 0x0000ffff)) {
                ++Count;
            }
        }
    }

    class Day15
    {
        static void Main()
        {
            string input = File.ReadAllText("input.txt").Trim();

            string inputRegex = @"Generator A starts with (\d+)\nGenerator B starts with (\d+)";
            Regex regex = new Regex(inputRegex);

            var match = regex.Match(input);

            Generator a = new Generator(uint.Parse(match.Groups[1].Value), 16807);
            Generator b = new Generator(uint.Parse(match.Groups[2].Value), 48271);

            Judge dredd = new Judge();

            for(int i = 0; i < 40000000; ++i) {
                dredd.Compare(a.Next(), b.Next());
            }

            Console.WriteLine(dredd.Count);
        }
    }
}
