using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aoc
{
    class Node
    {
        public string Name;
        public List<Node> Children;

        public Node(string name) {
            Name = name;
            Children = new List<Node>();
        }

        public void AddChild(Node child) {
            Children.Add(child);
        }
    }

    class Day12
    {
        static void Walk(Node root, List<Node> visited, ref int acc) {
            Console.WriteLine(root.Name);
            acc++;
            visited.Add(root);

            foreach(Node child in root.Children) {
                if(!visited.Contains(child)) {
                    Walk(child, visited, ref acc);
                }
            }
        }

        static void Main()
        {
            // 2 <-> 0, 3, 4
            string lineRegex = @"(\d+) <-. (.+)";

            Regex regex = new Regex(lineRegex);

            Dictionary<string, Node> nodes = new Dictionary<string, Node>();

            foreach(string line in File.ReadLines("input.txt")) {
                Console.WriteLine(line);
                var match = regex.Match(line);

                Node newNode = new Node(match.Groups[1].Value);
                nodes[newNode.Name] = newNode;
            }

            foreach(string line in File.ReadLines("input.txt")) {
                var match = regex.Match(line);
                string[] children = Regex.Split(match.Groups[2].Value, ", ");

                foreach(string child in children) {
                    nodes[match.Groups[1].Value].AddChild(nodes[child]);
                }
            }

            List<Node> visited = new List<Node>();
            int acc = 0;

            Walk(nodes["0"], visited, ref acc);

            Console.WriteLine(acc);
        }
    }
}
