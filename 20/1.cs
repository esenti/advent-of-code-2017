using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aoc
{
    class Point
    {
        public float X;
        public float Y;
        public float Z;

        public Point(float x, float y, float z)
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

        public Particle(int id, Point position, Point velocity, Point acceleration)
        {
            Id = id;
            Position = position;
            Velocity = velocity;
            Acceleration = acceleration;
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
        static void Process(object obj)
        {
            var args = (Tuple<List<Particle>, int>)obj;
            List<Particle> particles = args.Item1;
            int steps = args.Item2;

            for(int i = 0; i < steps; ++i)
            {
                foreach(Particle p in particles)
                {
                    p.Step();
                }
            }
        }

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
                    float.Parse(match.Groups["px"].Value),
                    float.Parse(match.Groups["py"].Value),
                    float.Parse(match.Groups["pz"].Value)
                );

                Point velocity = new Point(
                    float.Parse(match.Groups["vx"].Value),
                    float.Parse(match.Groups["vy"].Value),
                    float.Parse(match.Groups["vz"].Value)
                );
                Point acceleration = new Point(
                    float.Parse(match.Groups["ax"].Value),
                    float.Parse(match.Groups["ay"].Value),
                    float.Parse(match.Groups["az"].Value)
                );

                particles.Add(new Particle(index++, position, velocity, acceleration));
            }

            int steps = 100000;

            int numThreads = Math.Min(particles.Count, 8);
            int f = (int)Math.Round((float)particles.Count / numThreads);

            for(int j = 0; j < 10; ++j)
            {
                var threads = new List<Thread>();

                for(int i = 0; i < numThreads; ++i)
                {
                    int start = i * f;
                    int end = i == numThreads - 1 ? particles.Count - 1 : (i + 1) * f - 1;

                    List<Particle> range = particles.GetRange(start, end - start + 1);

                    var thread = new Thread(Process);
                    threads.Add(thread);
                    thread.Start(Tuple.Create(range, steps));
                }

                foreach(Thread thread in threads)
                {
                    thread.Join();
                }

                float minDistance = float.MaxValue;
                Particle closestParticle = null;
                Point zero = new Point(0, 0, 0);

                foreach(Particle p in particles)
                {
                    float d = p.Distance(zero);
                    if(d < minDistance)
                    {
                        minDistance = d;
                        closestParticle = p;
                    }
                }

                Console.WriteLine(closestParticle);
            }
        }
    }
}
