using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aoc
{
    class Day17
    {
        static void Main()
        {
            int input = int.Parse(File.ReadAllText("input.txt"));

            LinkedList<int> buffer = new LinkedList<int>();

            buffer.AddFirst(0);

            int count = 50000000;
            var node = buffer.First;

            for(int i = 1; i <= count; ++i) {

                for(int j = 0; j < input; ++j) {
                    node = node.Next;
                    if(node == null) {
                        node = buffer.First;
                    }
                }

                buffer.AddAfter(node, i);

                node = node.Next;
                if(node == null) {
                    node = buffer.First;
                }

                if(i % (count / 1000) == 0) {
                    Console.WriteLine("{0:0.00}%", i / (float)count * 100);
                }
            }

            Console.WriteLine(buffer.Find(0).Next.Value);
        }
    }
}
