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

        public enum State { CLEAN, WEAKENED, INFECTED, FLAGGED };

        static void Main()
        {
            int offset = 0;
            int x = 0;
            int y = 0;
            int infections = 0;

            var grid = new Dictionary<Point, State>();
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
                    grid[new Point(x++, y)] = c == '#' ? State.INFECTED : State.CLEAN;
                }

                ++y;
            }

            for(int step = 0; step < 10000000; ++step)
            {
                State state = State.CLEAN;

                if(!grid.ContainsKey(position) || grid[position] == State.CLEAN)
                {
                    int index = (int)direction == 0 ? Enum.GetNames(typeof(Direction)).Length - 1 : (int)direction - 1;
                    direction = (Direction)index;
                }
                else if(grid[position] == State.WEAKENED)
                {
                    state = grid[position];
                    ++infections;
                }
                else if(grid[position] == State.INFECTED)
                {
                    state = grid[position];
                    direction = (Direction)(((int)direction + 1) % Enum.GetNames(typeof(Direction)).Length);
                }
                else if(grid[position] == State.FLAGGED)
                {
                    state = grid[position];
                    direction = (Direction)(((int)direction + 2) % Enum.GetNames(typeof(Direction)).Length);
                }

                grid[(Point)position.Clone()] = (State)(((int)state + 1) % Enum.GetNames(typeof(State)).Length);

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
            }

            Console.WriteLine(infections);
        }
    }
}
