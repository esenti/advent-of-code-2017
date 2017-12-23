using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    abstract class Move {
        public abstract void Perform(List<char> input);
    }

    class Spin: Move {
        private uint count;

        public Spin(string input) {
            count = uint.Parse(input);
       }

        public override void Perform(List<char> input) {
            List<char> tmp = new List<char>();

            for(int i = 0; i < count; ++i) {
                tmp.Add(input[input.Count - 1]);
                input.RemoveAt(input.Count - 1);
            }

            for(int i = 0; i < count; ++i) {
                input.Insert(0, tmp[i]);
            }

        }

        public override string ToString() {
            return $"Spin: {count}";
        }
    }

    class Exchange: Move {
        private int a;
        private int b;

        public Exchange(string input) {
            string[] s = input.Split('/');

            a = int.Parse(s[0]);
            b = int.Parse(s[1]);
        }

        public override void Perform(List<char> input) {
            char tmp = input[a];
            input[a] = input[b];
            input[b] = tmp;
        }

        public override string ToString() {
            return $"Exchange: {a} <-> {b}";
        }
    }

    class Partner: Move {
        private char a;
        private char b;

        public Partner(string input) {
            string[] s = input.Split('/');

            a = s[0][0];
            b = s[1][0];
        }

        public override void Perform(List<char> input) {
            int ia = input.IndexOf(a);
            int ib = input.IndexOf(b);

            char tmp = input[ia];
            input[ia] = input[ib];
            input[ib] = tmp;
        }

        public override string ToString() {
            return $"Partner: {a} <-> {b}";
        }
    }

    class Day16
    {
        static void Main()
        {
            string[] input = File.ReadAllText("input.txt").Trim().Split(',');

            List<Move> moves = new List<Move>();

            foreach(string m in input) {
                char type = m[0];
                Move move = null;

                switch(type) {
                    case 's':
                        move = new Spin(m.Substring(1));
                        break;
                    case 'x':
                        move = new Exchange(m.Substring(1));
                        break;
                    case 'p':
                        move = new Partner(m.Substring(1));
                        break;
                }

                moves.Add(move);
            }

            List<char> line = new List<char>("abcdefghijklmnop");

            foreach(Move move in moves) {
                Console.WriteLine(move);
                move.Perform(line);
            }

            Console.WriteLine(string.Join("", line));
        }
    }
}
