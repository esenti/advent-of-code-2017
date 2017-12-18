using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aoc
{
    class Hex {
        public int x;
        public int y;
        public int z;

        public Hex(int x_, int y_, int z_) {
            x = x_;
            y = y_;
            z = z_;
        }

        public void Move(string move) {
            switch(move) {
                case "n":
                    y++;
                    z--;
                    break;
                case "ne":
                    x++;
                    z--;
                    break;
                case "se":
                    x++;
                    y--;
                    break;
                case "s":
                    y--;
                    z++;
                    break;
                case "sw":
                    x--;
                    z++;
                    break;
                case "nw":
                    x--;
                    y++;
                    break;
            }
        }

        public int Distance(Hex other) {
            return (Math.Abs(x - other.x) + Math.Abs(y - other.y) + Math.Abs(z - other.z)) / 2;
        }

        public override string ToString() {
            return $"{x}, {y}, {z}";
        }
    }
    class Day11
    {
        static void Main()
        {
            string[] moves = File.ReadAllText("input.txt").Trim().Split(',');

            Hex position = new Hex(0, 0 ,0);

            int maxDistance = 0;

            foreach(string move in moves) {
                position.Move(move);

                int distance = position.Distance(new Hex(0, 0, 0));
                maxDistance = Math.Max(distance, maxDistance);
            }

            Console.WriteLine(position);
            Console.WriteLine(maxDistance);
        }
    }
}
