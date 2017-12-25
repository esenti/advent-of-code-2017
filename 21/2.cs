using System;
using System.IO;
using System.Collections.Generic;

namespace Aoc
{
    class Matrix<T>
    {
        T[,] data;
        public int Size;

        public Matrix(int size)
        {
            data = new T[size, size];
            Size = size;
        }

        public Matrix(T[,] initial)
        {
            if(initial.GetLength(0) != initial.GetLength(1))
            {
                throw new ArgumentException("Only square matrices are supported.");
            }

            data = initial;
            Size = initial.GetLength(0);
        }

        public Matrix(T[][] initial)
        {
            data = new T[initial.Length, initial.Length];
            Size = initial.Length;

            for(int i = 0; i < Size; ++i)
            {
                for(int j = 0; j < Size; ++j)
                {
                    data[i, j] = initial[i][j];
                }
            }
        }

        public int Count(T v)
        {
            int result = 0;

            for(int i = 0; i < Size; ++i)
            {
                for(int j = 0; j < Size; ++j)
                {
                    if(data[i, j].Equals(v))
                    {
                        ++result;
                    }
                }
            }

            return result;
        }

        public Matrix<T>[,] Divide(int s)
        {
            int count = Size / s;
            Matrix<T>[,] result = new Matrix<T>[count, count];

            for(int x = 0; x < count; ++x)
            {
                for(int y = 0; y < count; ++y)
                {
                    result[x, y] = new Matrix<T>(s);

                    for(int i = 0; i < s; ++i)
                    {
                        for(int j = 0; j < s; ++j)
                        {
                            result[x, y].data[i, j] = data[s * x + i, s * y + j];
                        }
                    }
                }
            }

            return result;
        }

        public Matrix<T> FlipHorizontally()
        {
            T[,] result = (T[,])data.Clone();

            for(int i = 0; i < Size; ++i)
            {
                for(int j = 0; j < Size; ++j)
                {
                    result[i, j] = data[i, Size - 1 - j];
                }
            }

            return new Matrix<T>(result);
        }

        public Matrix<T> FlipVertically()
        {
            T[,] result = (T[,])data.Clone();

            for(int i = 0; i < Size; ++i)
            {
                for(int j = 0; j < Size; ++j)
                {
                    result[i, j] = data[Size - 1 - i, j];
                }
            }

            return new Matrix<T>(result);
        }

        public Matrix<T> RotateClockwise()
        {
            T[,] result = (T[,])data.Clone();

            for(int i = 0; i < Size; ++i)
            {
                for(int j = 0; j < Size; ++j)
                {
                    result[i, j] = data[Size - 1 - j, i];
                }
            }

            return new Matrix<T>(result);
        }

        public static Matrix<T> Join(Matrix<T>[,] matrices)
        {
            Matrix<T> result = new Matrix<T>(matrices[0,0].Size * matrices.GetLength(0));
            int s = matrices[0,0].Size;

            for(int i = 0; i < result.Size; ++i)
            {
                for(int j = 0; j < result.Size; ++j)
                {
                    result.data[i, j] = matrices[i / s, j / s].data[i % s, j % s];
                }
            }

            return result;
        }

        public override string ToString()
        {
            string result = "";

            for(int i = 0; i < Size; ++i)
            {
                for(int j = 0; j < Size; ++j)
                {
                    result += data[i, j] + "\t";
                }

                result += '\n';
            }

            return result;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Matrix<T>);
        }

        public bool Equals(Matrix<T> other)
        {
            for(int i = 0; i < Size; ++i)
            {
                for(int j = 0; j < Size; ++j)
                {
                    if(!data[i, j].Equals(other.data[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }

    class Day21
    {
        static void Main()
        {
            var patterns = new Dictionary<Matrix<char>, Matrix<char>>();

            foreach(string line in File.ReadLines("input.txt"))
            {
                Console.WriteLine(line);
                string[] split = line.Split(new string[] {" => "}, StringSplitOptions.None);

                char[][] from = Array.ConvertAll(split[0].Split('/'), x => x.ToCharArray());
                char[][] to = Array.ConvertAll(split[1].Split('/'), x => x.ToCharArray());

                Matrix<char> fromPattern = new Matrix<char>(from);
                Matrix<char> toPattern = new Matrix<char>(to);

                patterns[fromPattern] = toPattern;
            }

            Matrix<char> grid = new Matrix<char>(new char[,] {{'.', '#', '.'}, {'.', '.', '#'}, {'#', '#', '#'}});

            for(int step = 0; step < 18; ++step)
            {
                Console.WriteLine(step);

                Matrix<char>[,] divided = null;

                if(grid.Size % 2 == 0)
                {
                    divided = grid.Divide(2);
                }
                else if(grid.Size % 3 == 0)
                {
                    divided = grid.Divide(3);
                }

                for(int i = 0; i < divided.GetLength(0); ++i)
                {
                    for(int j = 0; j < divided.GetLength(1); ++j)
                    {
                        var matrix = divided[i, j];

                        for(int k = 0; k < 3; ++k)
                        {
                            matrix = matrix.FlipHorizontally();

                            if(patterns.ContainsKey(matrix))
                            {
                                break;
                            }

                            matrix = matrix.FlipVertically();

                            if(patterns.ContainsKey(matrix))
                            {
                                break;
                            }

                            matrix = matrix.FlipHorizontally();

                            if(patterns.ContainsKey(matrix))
                            {
                                break;
                            }

                            matrix = matrix.FlipVertically();

                            if(patterns.ContainsKey(matrix))
                            {
                                break;
                            }

                            matrix = matrix.RotateClockwise();
                        }

                        divided[i, j] = patterns[matrix];
                    }
                }

                grid = Matrix<char>.Join(divided);
            }

            Console.WriteLine(grid.Count('#'));
        }
    }
}

