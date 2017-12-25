using System;
using System.IO;
using System.Collections.Generic;

namespace Aoc
{
    class Point: IEquatable<Point>
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override int GetHashCode()
        {
            return 100000 * X + Y;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Point);
        }

        public bool Equals(Point other)
        {
            return other != null && X == other.X && Y == other.Y;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }

    class Day22
    {
        public enum Direction { UP, RIGHT, DOWN, LEFT };

        static void Main()
        {
            int offset = 0;
            int x = 0;
            int y = 0;
            int infections = 0;

            var grid = new Dictionary<Point, bool>();
            Direction direction = Direction.UP;
            Point position = new Point(0, 0);

            foreach(string line in File.ReadLines("input.txt"))
            {
                if(offset == 0)
                {
                    offset = line.Length / 2;
                    x = -offset;
                    y = -offset;
                }

                x = -offset;

                foreach(char c in line)
                {
                    grid[new Point(x++, y)] = c == '#';
                }

                ++y;
            }

            for(int step = 0; step < 10000; ++step)
            {
                if(grid.ContainsKey(position) && grid[position])
                {
                    grid[(Point)position.Clone()] = false;
                    direction = (Direction)(((int)direction + 1) % Enum.GetNames(typeof(Direction)).Length);
                    Console.WriteLine("turning right: {0}", direction);
                }
                else
                {
                    grid[(Point)position.Clone()] = true;
                    ++infections;
                    int index = (int)direction == 0 ? Enum.GetNames(typeof(Direction)).Length - 1 : (int)direction - 1;
                    direction = (Direction)index;
                    Console.WriteLine("turning left: {0}", direction);
                }

                switch(direction)
                {
                    case Direction.UP:
                        --position.Y;
                        break;
                    case Direction.RIGHT:
                        ++position.X;
                        break;
                    case Direction.DOWN:
                        ++position.Y;
                        break;
                    case Direction.LEFT:
                        --position.X;
                        break;
                }

                Console.WriteLine(direction);
                Console.WriteLine(position);
            }

            Console.WriteLine(infections);
        }
    }
}
