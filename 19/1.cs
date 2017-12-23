using System;
using System.IO;
using System.Collections.Generic;

namespace Aoc
{
    class Thing
    {
        public bool Up;
        public bool Down;
        public bool Left;
        public bool Right;
        public char C;

        public Thing(char c)
        {
            C = c;
        }

        public bool IsLetter()
        {
            return C >= 'A' && C <= 'Z';
        }

        public override string ToString()
        {
            return $"{C} {Up} {Right} {Down} {Left}";
        }
    }


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

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }

    class Day19
    {
        static void Main()
        {
            Point position = new Point(-1, 0);

            var things = new Dictionary<Point, Thing>();
            var input = new List<List<char>>();

            int width = 0;
            int height = 0;

            foreach(string line in File.ReadLines("input.txt"))
            {
                var l = new List<char>();
                input.Add(l);

                foreach(char c in line)
                {
                    l.Add(c);
                }
            }

            for(int y = 0; y < input.Count; ++y)
            {
                for(int x = 0; x < input[y].Count; ++x)
                {
                    char c = input[y][x];

                    if(position.X == -1 && c == '|')
                    {
                        position.X = x;
                    }

                    if(c == '+' || (c >= 'A' && c <= 'Z'))
                    {
                        Thing thing = new Thing(c);

                        if(c == '+')
                        {
                            thing.Up = (y - 1 >= 0 && x < input[y - 1].Count && (input[y - 1][x] == '|' || input[y - 1][x] >= 'A' && input[y - 1][x] <= 'Z'));
                            thing.Down = (y + 1 < input.Count && x < input[y + 1].Count && (input[y + 1][x] == '|' || input[y + 1][x] >= 'A' && input[y + 1][x] <= 'Z'));
                            thing.Left = (x - 1 >= 0 && (input[y][x - 1] == '-' || input[y][x - 1] >= 'A' && input[y][x - 1] <= 'Z'));
                            thing.Right = (x + 1 < input[y].Count && (input[y][x + 1] == '-' || input[y][x + 1] >= 'A' && input[y][x + 1] <= 'Z'));
                        }

                        things[new Point(x, y)] = thing;
                    }

                    if(x > width)
                    {
                        width = x;
                    }

                    if(y > height)
                    {
                        height = y;
                    }
                }
            }

            ++width;
            ++height;

            foreach (KeyValuePair<Point, Thing> kvp in things)
            {
                Console.WriteLine("{0}: {1}", kvp.Key, kvp.Value);
            }

            bool run = true;
            string direction = "down";
            var letters = new List<char>();

            while(run)
            {
                switch(direction)
                {
                    case "up":
                        --position.Y;
                        break;
                    case "right":
                        ++position.X;
                        break;
                    case "down":
                        ++position.Y;
                        break;
                    case "left":
                        --position.X;
                        break;
                }

                run = (position.X >= 0 && position.X < width && position.Y >= 0 && position.Y < height);

                Console.WriteLine(position);

                if(things.ContainsKey(position))
                {
                    Thing thing = things[position];

                    if(thing.IsLetter())
                    {
                        if(letters.Contains(thing.C))
                        {
                            break;
                        }

                        letters.Add(thing.C);
                        continue;
                    }

                    if((direction == "down" && !thing.Down) || (direction == "up" && !thing.Up))
                    {
                        if(thing.Right)
                        {
                            direction = "right";
                        }
                        else if(thing.Left)
                        {
                            direction = "left";
                        }
                    }
                    else if((direction == "right" && !thing.Right) || (direction == "left" && !thing.Left))
                    {
                        if(thing.Up)
                        {
                            direction = "up";
                        }
                        else if(thing.Down)
                        {
                            direction = "down";
                        }
                    }
                }
            }

            Console.WriteLine(string.Join("", letters));
        }
    }
}
