using System;
using System.Collections.Generic;

namespace Aoc
{
    class Day3
    {
        static int Add(Dictionary<Tuple<int, int>, int> map, int x, int y) {
            int result = 0;

            for(int i = -1; i < 2; i++) {
                for(int j = -1; j < 2; j++) {
                    if(i == 0 && j == 0) {
                        continue;
                    }

                    Tuple<int, int> key = Tuple.Create(x + i, y + j);

                    if(map.ContainsKey(key)) {
                        result += map[key];
                    }
                }
            }

            return result;
        }

        static int Build(int num) {

            Dictionary<Tuple<int, int>, int> map = new Dictionary<Tuple<int, int>, int>();

            int v = 0;

            int x = 1;
            int y = 0;

            map[Tuple.Create(0, 0)] = 1;

            int step = 1;

            while(v < num) {
                while(y >= -step) {
                    v = Add(map, x, y);
                    Console.WriteLine("({0}, {1}): {2}", x, y, v);
                    map[Tuple.Create(x, y)] = v;

                    if(v > num) {
                        return v;
                    }

                    y--;
                }

                x--;
                y++;

                while(x >= -step) {
                    v = Add(map, x, y);
                    Console.WriteLine("({0}, {1}): {2}", x, y, v);
                    map[Tuple.Create(x, y)] = v;

                    if(v > num) {
                        return v;
                    }

                    x--;
                }

                x++;
                y++;

                while(y <= step) {
                    v = Add(map, x, y);
                    Console.WriteLine("({0}, {1}): {2}", x, y, v);
                    map[Tuple.Create(x, y)] = v;

                    if(v > num) {
                        return v;
                    }

                    y++;
                }

                x++;
                y--;

                while(x <= step) {
                    v = Add(map, x, y);
                    Console.WriteLine("({0}, {1}): {2}", x, y, v);
                    map[Tuple.Create(x, y)] = v;

                    if(v > num) {
                        return v;
                    }

                    x++;
                }

                step++;
            }

            return 0;
        }

        static void Main()
        {
            String s = Console.ReadLine();

            int num = int.Parse(s);

            int result = Build(num);

            Console.WriteLine(result);
        }
    }
}

