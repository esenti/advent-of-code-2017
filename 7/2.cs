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
        public int Weight;
        public List<Node> Children;

        public Node(string name, int weight) {
            Name = name;
            Weight = weight;
            Children = new List<Node>();
        }

        public void AddChild(Node child) {
            Children.Add(child);
        }

        public int CalculateWeight() {
            int acc = Weight;

            foreach(Node child in Children) {
                acc += child.CalculateWeight();
            }

            return acc;
        }
    }

    class Day7
    {
        static void Walk(Node root, int weight=0) {
            Console.WriteLine("Walking {0}", root.Name);

            List<int> weights = new List<int>();

            foreach(Node child in root.Children) {
                weights.Add(child.CalculateWeight());
            }

            int wrongChild = -1;
            int properWeigth = 0;

            foreach(var g in weights.GroupBy(i => i)) {
                if(g.Count() == 1) {
                    wrongChild = weights.IndexOf(g.Key);
                } else {
                    properWeigth = g.Key;
                }
            }

            if(wrongChild == -1) {
                Console.WriteLine(root.Name);
                Console.WriteLine(root.Weight + weight - root.CalculateWeight());
            } else {
                Walk(root.Children[wrongChild], properWeigth);
            }
        }

        static void Main()
        {
            // fwft (72) -> ktlj, cntj, xhth
            string lineRegex = @"(\w+) \((\d+)\)(?: -> (.+))?";

            Regex regex = new Regex(lineRegex);

            Dictionary<string, Node> nodes = new Dictionary<string, Node>();
            HashSet<string> possibleRoots = new HashSet<string>();

            foreach(string line in File.ReadLines("input.txt")) {
                Console.WriteLine(line);
                var match = regex.Match(line);

                Console.WriteLine(match.Groups[1]);
                Console.WriteLine(match.Groups[2]);

                Node newNode = new Node(match.Groups[1].Value, int.Parse(match.Groups[2].Value));
                nodes[newNode.Name] = newNode;
                possibleRoots.Add(newNode.Name);
            }

            foreach(string line in File.ReadLines("input.txt")) {
                var match = regex.Match(line);
                if(match.Groups[3].Success) {
                    string[] children = Regex.Split(match.Groups[3].Value, ", ");

                    foreach(string child in children) {
                        possibleRoots.Remove(child);
                        nodes[match.Groups[1].Value].AddChild(nodes[child]);
                    }
                }
            }

            Node root = nodes[possibleRoots.First()];

            Walk(root);
        }
    }
}
