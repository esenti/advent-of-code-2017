using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aoc
{
    class Instruction
    {
        public enum Type { SND, SET, ADD, MUL, MOD, RCV, JGZ };

        public Type type;
        char ar;
        long av;
        char br;
        long bv;

        public Instruction(string t, string a_, string b_)
        {
            type = (Type)Enum.Parse(typeof(Type), t.ToUpper());
            try
            {
                av = long.Parse(a_);
                ar = (char)0;
            }
            catch(FormatException)
            {
                ar = a_[0];
            }

            try
            {
                bv = long.Parse(b_);
                br = (char)0;
            }
            catch(FormatException)
            {
                br = b_[0];
            }
        }

        public int Execute(Dictionary<char, long> registers, Queue<long> inQueue, Queue<long> outQueue)
        {
            switch(type)
            {
                case Type.SND:
                    ++registers['_'];
                    outQueue.Enqueue(Value(registers));
                    return 1;
                case Type.SET:
                    registers[ar] = Value(registers);
                    return 1;
                case Type.ADD:
                    registers[ar] += Value(registers);
                    return 1;
                case Type.MUL:
                    registers[ar] *= Value(registers);
                    return 1;
                case Type.MOD:
                    registers[ar] %= Value(registers);
                    return 1;
                case Type.RCV:
                    if(inQueue.Count > 0)
                    {
                        registers[br] = inQueue.Dequeue();
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                case Type.JGZ:
                    return Argument(registers) > 0 ? (int)Value(registers) : 1;
            }

            Console.WriteLine(type);
            return 10000000;
        }

        protected long Value(Dictionary<char, long> registers)
        {
            if(br != 0)
            {
                return registers[br];
            }
            else
            {
                return bv;
            }
        }

        protected long Argument(Dictionary<char, long> registers)
        {
            if(ar != 0)
            {
                return registers[ar];
            }
            else
            {
                return av;
            }
        }

        public override string ToString()
        {
            return $"{type} {ar} {av} {br} {bv}";
        }
    }

    class Day18
    {
        static void Main()
        {
            Dictionary<char, long> registersA = new Dictionary<char, long>();
            Dictionary<char, long> registersB = new Dictionary<char, long>();
            List<Instruction> instructions = new List<Instruction>();

            Queue<long> queueA = new Queue<long>();
            Queue<long> queueB = new Queue<long>();

            foreach(string line in File.ReadLines("input.txt"))
            {
                string[] s = line.Split();
                Instruction instruction;

                if(s.Length == 3)
                {
                    instruction = new Instruction(s[0], s[1], s[2]);
                    registersA[s[1][0]] = 0;
                    registersB[s[1][0]] = 0;
                }
                else
                {
                    instruction = new Instruction(s[0], " ", s[1]);
                }

                instructions.Add(instruction);
            }

            registersA['p'] = 0;
            registersB['p'] = 1;

            registersA['_'] = 0;
            registersB['_'] = 0;

            int pointerA = 0;
            int pointerB = 0;
            int offsetA = 0;
            int offsetB = 0;

            foreach(Instruction i in instructions)
            {
                Console.WriteLine(i);
            }

            while(true)
            {
                if(pointerA >= 0 && pointerA < instructions.Count)
                {
                    offsetA = instructions[pointerA].Execute(registersA, queueA, queueB);
                    pointerA += offsetA;
                }
                else
                {
                    offsetA = 0;
                }

                if(pointerB >= 0 && pointerB < instructions.Count)
                {
                    offsetB = instructions[pointerB].Execute(registersB, queueB, queueA);
                    pointerB += offsetB;
                }
                else
                {
                    offsetB = 0;
                }

                if(offsetA == 0 && offsetB == 0)
                {
                    break;
                }
            }

            foreach (KeyValuePair<char, long> kvp in registersB)
            {
                Console.WriteLine("{0}: {1}", kvp.Key, kvp.Value);
            }
        }
    }
}
