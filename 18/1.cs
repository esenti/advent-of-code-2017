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
        char a;
        char br;
        long bv;

        public Instruction(string t, char a_, string b_)
        {
            type = (Type)Enum.Parse(typeof(Type), t.ToUpper());
            a = a_;

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

        public int Execute(Dictionary<char, long> registers)
        {
            switch(type)
            {
                case Type.SND:
                    registers['_'] = Value(registers);
                    return 1;
                case Type.SET:
                    registers[a] = Value(registers);
                    return 1;
                case Type.ADD:
                    registers[a] += Value(registers);
                    return 1;
                case Type.MUL:
                    registers[a] *= Value(registers);
                    return 1;
                case Type.MOD:
                    registers[a] %= Value(registers);
                    return 1;
                case Type.RCV:
                    return Value(registers) > 0 ? 10000000 : 1;
                case Type.JGZ:
                    return registers[a] > 0 ? (int)Value(registers) : 1;
            }

            return 1;
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

        public override string ToString()
        {
            return $"{type} {a} {br} {bv}";
        }
    }

    class Day18
    {
        static void Main()
        {
            Dictionary<char, long> registers = new Dictionary<char, long>();
            List<Instruction> instructions = new List<Instruction>();

            foreach(string line in File.ReadLines("input.txt"))
            {
                string[] s = line.Split();
                Instruction instruction;

                if(s.Length == 3)
                {
                    instruction = new Instruction(s[0], s[1][0], s[2]);
                    registers[s[1][0]] = 0;
                }
                else
                {
                    instruction = new Instruction(s[0], ' ', s[1]);
                }

                instructions.Add(instruction);
            }

            foreach (KeyValuePair<char, long> kvp in registers)
            {
                Console.WriteLine("{0}: {1}", kvp.Key, kvp.Value);
            }

            foreach(Instruction i in instructions)
            {
                Console.WriteLine(i);
            }

            int pointer = 0;

            while(true)
            {
                int offset = instructions[pointer].Execute(registers);
                Console.WriteLine(instructions[pointer]);

                pointer += offset;

                foreach (KeyValuePair<char, long> kvp in registers)
                {
                    Console.WriteLine("{0}: {1}", kvp.Key, kvp.Value);
                }

                if(pointer < 0 || pointer >= instructions.Count)
                {
                    break;
                }
            }
        }
    }
}
