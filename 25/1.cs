using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aoc
{
    class Rule
    {
        int value0;
        int offset0;
        char next0;
        int value1;
        int offset1;
        char next1;

        public Rule(int value0_, int offset0_, char next0_, int value1_, int offset1_, char next1_)
        {
            value0 = value0_;
            offset0 = offset0_;
            next0 = next0_;
            value1 = value1_;
            offset1 = offset1_;
            next1 = next1_;
        }

        public char Run(Dictionary<int, int> tape, ref int cursor)
        {
            if(!tape.ContainsKey(cursor))
            {
                tape[cursor] = 0;
            }

            if(tape[cursor] == 0)
            {
                tape[cursor] = value0;
                cursor += offset0;
                return next0;
            }
            else if(tape[cursor] == 1)
            {
                tape[cursor] = value1;
                cursor += offset1;
                return next1;
            }

            return '\0';
        }
    }

    class Day25
    {
        static void Main()
        {
            string stateFormat = @"In state (?<state>\w):
  If the current value is 0:
    - Write the value (?<value0>\d+)\.
    - Move one slot to the (?<move0>\w+)\.
    - Continue with state (?<next0>\w)\.
  If the current value is 1:
    - Write the value (?<value1>\d+)\.
    - Move one slot to the (?<move1>\w+)\.
    - Continue with state (?<next1>\w)\.";

            Regex stateRegex = new Regex(stateFormat);

            var rules = new Dictionary<char, Rule>();
            var tape = new Dictionary<int, int>();
            int cursor = 0;

            string input = File.ReadAllText("input.txt");

            Regex initialRegex = new Regex(@"Begin in state (?<state>\w).");
            char state = initialRegex.Match(input).Groups["state"].Value[0];

            Regex stepsRegex = new Regex(@"Perform a diagnostic checksum after (?<steps>\d+) steps.");
            int steps = int.Parse(stepsRegex.Match(input).Groups["steps"].Value);

            foreach(Match match in stateRegex.Matches(input))
            {
                Console.WriteLine(match.Value);

                Rule rule = new Rule(
                    int.Parse(match.Groups["value0"].Value),
                    match.Groups["move0"].Value == "right" ? 1 : -1,
                    match.Groups["next0"].Value[0],
                    int.Parse(match.Groups["value1"].Value),
                    match.Groups["move1"].Value == "right" ? 1 : -1,
                    match.Groups["next1"].Value[0]
                );

                rules[match.Groups["state"].Value[0]] = rule;
            }

            for(int i = 0; i < steps; ++i)
            {
                state = rules[state].Run(tape, ref cursor);
            }

            int checksum = 0;

            foreach(KeyValuePair<int, int> kvp in tape)
            {
                if(kvp.Value == 1)
                {
                    ++checksum;
                }
            }

            Console.WriteLine(checksum);
        }
    }
}
