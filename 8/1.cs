using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aoc
{
    class Day8
    {
        static bool Operator(string op, int a, int b) {
            switch(op) {
                case "==": return a == b;
                case ">": return a > b;
                case "<": return a < b;
                case ">=": return a >= b;
                case "<=": return a <= b;
                case "!=": return a != b;
            }

            return false;
        }

        static void Main()
        {
            // ioe dec 890 if qk > -10
            string lineRegex = @"(\w+) (\w+) (-?\d+) if (\w+) ([><!=]+) (-?\d+)";

            Regex regex = new Regex(lineRegex);

            Dictionary<string, int> registers = new Dictionary<string, int>();

            foreach(string line in File.ReadLines("input.txt")) {
                Console.WriteLine(line);
                var match = regex.Match(line);

                string register = match.Groups[1].Value;
                string op = match.Groups[2].Value;
                int val = int.Parse(match.Groups[3].Value);
                string ifRegister = match.Groups[4].Value;
                string ifOp = match.Groups[5].Value;
                int ifVal = int.Parse(match.Groups[6].Value);

                if(!registers.ContainsKey(register)) {
                    registers[register] = 0;
                }

                if(!registers.ContainsKey(ifRegister)) {
                    registers[ifRegister] = 0;
                }

                if(Operator(ifOp, registers[ifRegister], ifVal)) {
                    int f = op == "inc" ? 1 : -1;

                    registers[register] += f * val;
                }
            }

            Console.WriteLine(registers.Values.Max());
        }
    }
}
