using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    class Move {
        public enum Type { SPIN, EXCHANGE, PARTNER }

        private uint count;
        private int a;
        private int b;
        private int ia;
        private int ib;
        private char tmp;
        private Type type;

        public Move(Type type_, string input) {
            type = type_;
            string[] s = input.Split('/');

            switch(type) {
                case Type.SPIN:
                    count = uint.Parse(input);
                    break;
                case Type.EXCHANGE:
                    a = int.Parse(s[0]);
                    b = int.Parse(s[1]);
                    break;
                case Type.PARTNER:
                    a = s[0][0];
                    b = s[1][0];
                    break;
            }

        }

        public void Perform(char[] input, ref int offset) {
            switch(type) {
                case Type.SPIN:
                    offset = (int)(offset + (input.Length - count)) % input.Length;
                    return;
                case Type.EXCHANGE:
                    ia = (a + offset) % input.Length;
                    ib = (b + offset) % input.Length;

                    tmp = input[ia];
                    input[ia] = input[ib];
                    input[ib] = tmp;
                    return;
                case Type.PARTNER:
                    ia = -1;
                    ib = -1;

                    for(int i = 0; i < input.Length; ++i) {
                        if(input[i] == a) {
                            ia = i;
                            if(ib != -1) {
                                break;
                            }
                        } else if(input[i] == b) {
                            ib = i;
                            if(ia != -1) {
                                break;
                            }
                        }

                    }

                    tmp = input[ia];
                    input[ia] = input[ib];
                    input[ib] = tmp;
                    return;
            }

        }

        public override string ToString() {
            return $"{type}";
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
                        move = new Move(Move.Type.SPIN, m.Substring(1));
                        break;
                    case 'x':
                        move = new Move(Move.Type.EXCHANGE, m.Substring(1));
                        break;
                    case 'p':
                        move = new Move(Move.Type.PARTNER, m.Substring(1));
                        break;
                }

                moves.Add(move);
            }

            char[] line = "abcdefghijklmnop".ToCharArray();
            int offset = 0;

            int count = 1000000000;

            Dictionary<Tuple<string, int>, Tuple<string, int>> c = new Dictionary<Tuple<string, int>, Tuple<string, int>>();

            for(int i = 0; i < count; ++i) {
                string s = string.Join("", line);
                if(c.ContainsKey(Tuple.Create(s, offset))) {

                    line = c[Tuple.Create(s, offset)].Item1.ToCharArray();
                    offset = c[Tuple.Create(s, offset)].Item2;
                } else {
                    int oldOffset = offset;

                    foreach(Move move in moves) {
                        move.Perform(line, ref offset);
                    }

                    c[Tuple.Create(s, oldOffset)] = Tuple.Create(string.Join("", line), offset);
                }

                if(i % (count / 1000) == 0) {
                    Console.WriteLine("{0:0.00}%", i / (float)count * 100);
                }
            }

            Console.WriteLine("---");
            for(int i = 0; i < line.Length; ++i) {
                Console.Write(line[(i + offset) % line.Length]);
            }
            Console.WriteLine("");
        }
    }
}
