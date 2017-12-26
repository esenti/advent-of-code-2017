using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aoc
{
    class Instruction
    {
        public enum Type { SET, SUB, MUL, JNZ };

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

        public int Execute(Dictionary<char, long> registers)
        {
            switch(type)
            {
                case Type.SET:
                    registers[ar] = Value(registers);
                    return 1;
                case Type.SUB:
                    registers[ar] -= Value(registers);
                    return 1;
                case Type.MUL:
                    ++registers['_'];
                    registers[ar] *= Value(registers);
                    return 1;
                case Type.JNZ:
                    return Argument(registers) != 0 ? (int)Value(registers) : 1;
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

    class Day23
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
                    instruction = new Instruction(s[0], s[1], s[2]);
                    registers[s[1][0]] = 0;
                }
                else
                {
                    instruction = new Instruction(s[0], " ", s[1]);
                }

                instructions.Add(instruction);
            }

            registers['_'] = 0;

            foreach(Instruction i in instructions)
            {
                Console.WriteLine(i);
            }

            int pointer = 0;

            while(true)
            {
                int  offset = instructions[pointer].Execute(registers);

                pointer += offset;

                if(pointer < 0 || pointer >= instructions.Count)
                {
                    break;
                }

            }

            foreach(KeyValuePair<char, long> kvp in registers)
            {
                Console.WriteLine("{0}: {1}", kvp.Key, kvp.Value);
            }
        }
    }
}
