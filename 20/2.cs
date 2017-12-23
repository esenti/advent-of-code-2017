using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aoc
{
    class Point
    {
        public long X;
        public long Y;
        public long Z;

        public Point(long x, long y, long z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public void Add(Point other)
        {
            X += other.X;
            Y += other.Y;
            Z += other.Z;
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }

    class Particle
    {
        public Point Position;
        public Point Velocity;
        public Point Acceleration;
        public int Id;
        public bool Destroyed;

        public Particle(int id, Point position, Point velocity, Point acceleration)
        {
            Id = id;
            Position = position;
            Velocity = velocity;
            Acceleration = acceleration;
        }

        public void Destroy()
        {
            Destroyed = true;
        }

        public bool CollidesWith(Particle other)
        {
            return Position.X == other.Position.X && Position.Y == other.Position.Y && Position.Z == other.Position.Z;
        }

        public void Step()
        {
            Velocity.Add(Acceleration);
            Position.Add(Velocity);
        }

        public float Distance(Point point)
        {
            return Math.Abs(Position.X - point.X) +Math.Abs(Position.Y - point.Y) + Math.Abs(Position.Z - point.Z);
        }

        public override string ToString()
        {
            return $"id={Id}, p={Position}, v={Velocity}, a={Acceleration}";
        }
    }

    class Day20
    {
        static void Main()
        {
            // p=<-3787,-3683,3352>, v=<41,-25,-124>, a=<5,9,1>
            string lineRegex = @"p=<(?<px>-?\d+),(?<py>-?\d+),(?<pz>-?\d+)>, v=<(?<vx>-?\d+),(?<vy>-?\d+),(?<vz>-?\d+)>, a=<(?<ax>-?\d+),(?<ay>-?\d+),(?<az>-?\d+)>";
            Regex regex = new Regex(lineRegex);

            var particles = new List<Particle>();
            int index = 0;

            foreach(string line in File.ReadLines("input.txt"))
            {
                var match = regex.Match(line);

                Point position = new Point(
                    long.Parse(match.Groups["px"].Value),
                    long.Parse(match.Groups["py"].Value),
                    long.Parse(match.Groups["pz"].Value)
                );

                Point velocity = new Point(
                    long.Parse(match.Groups["vx"].Value),
                    long.Parse(match.Groups["vy"].Value),
                    long.Parse(match.Groups["vz"].Value)
                );
                Point acceleration = new Point(
                    long.Parse(match.Groups["ax"].Value),
                    long.Parse(match.Groups["ay"].Value),
                    long.Parse(match.Groups["az"].Value)
                );

                particles.Add(new Particle(index++, position, velocity, acceleration));
            }

            int steps = 100;

            for(int j = 0; j < 10; ++j)
            {
                for(int i = 0; i < steps; ++i)
                {
                    foreach(Particle p in particles)
                    {
                        p.Step();
                    }

                    foreach(Particle a in particles)
                    {
                        foreach(Particle b in particles)
                        {
                            if(a != b && a.CollidesWith(b))
                            {
                                a.Destroy();
                                b.Destroy();
                            }
                        }
                    }

                    particles.RemoveAll(x => x.Destroyed);
                }

                Console.WriteLine(particles.Count);
            }
        }
    }
}
