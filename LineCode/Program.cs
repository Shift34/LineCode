using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Line_Code__5_2_
{
    internal class Program
    {
        private static readonly int[,] G = new int[2, 5]
        {
            { 1, 0, 1, 0, 1 },
            { 0, 1, 0, 1, 1 }
        };

        private static readonly int[,] Transpant_H = new int[5, 3]
        {
            { 1, 0, 1 },
            { 0, 1, 1 },
            { 1, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 1 }
        };

        private static readonly Dictionary<string, int> Syndromes = new Dictionary<string, int>
        {
            { "000", 0 },
            { "101", 1 },
            { "011", 2 },
            { "100", 3 },
            { "010", 4 },
            { "001", 5 },
            { "110", -1 },
            { "111", -1 }
        };

        static void Main()
        {
            for (int i = 0; i <= 1; i++)
            {
                for (int j = 0; j <= 1; j++)
                {
                    var list = new List<int> { i, j };
                    var enc = Multiply_Matrix(list, G);

                    for (int e = 0; e < 5; e++)
                    {
                        var encCopy = enc.ToList();
                        encCopy[e] = (encCopy[e] + 1) % 2;

                        Console.WriteLine($"Message: {i}{j}");
                        Console.WriteLine($"Encoded: {enc.ToStringJoin()}");
                        Console.WriteLine($"Error in {e + 1}");
                        Console.WriteLine($"Message with error: {encCopy.ToStringJoin()}");
                        var dec = Decoding(encCopy);
                        Console.WriteLine($"Decoded: {dec.ToStringJoin()}");
                        Console.WriteLine(new string('-', 30));
                    }
                }
            }

            Console.ReadKey();
            return;

            //Console.WriteLine("Введите сообщение:");
            //var input = Console.ReadLine();

            //if (!Regex.IsMatch(input, @"^[01]+$"))
            //{
            //    Console.WriteLine("Ошибка ввода");
            //    Console.ReadKey();
            //    return;
            //}

            //var inputParsed = new List<int>
            //{
            //    input[0] - '0',
            //    input[1] - '0'
            //};

            //var encoded = Multiply_Matrix(inputParsed, G);

            //Console.WriteLine("Закодированное сообщение:");
            //Console.WriteLine(encoded.ToStringJoin());

            //Console.WriteLine("Введите индекс ошибки:");
            //var errorIndex = int.Parse(Console.ReadLine()) - 1;
            //if (errorIndex != -1)
            //{
            //    encoded[errorIndex] = (encoded[errorIndex] + 1) % 2;
            //}
            //else
            //{
            //    return;
            //}

            //var syndrome = Multiply_Matrix(encoded, Transpant_H).ToStringJoin();

            //var errorVector = Enumerable.Repeat(0, 5).ToList();
            //errorVector[Syndromes[syndrome] - 1] = 1;

            ////if (Syndromes[syndrome] > 0)
            ////{
            ////    Console.WriteLine($"Ошибка в {Syndromes[syndrome]}-м бите");
            ////    Console.WriteLine($"Синдром: {syndrome}");
            ////    Console.WriteLine($"Вектор ошибки: {errorVector.ToStringJoin()}");
            ////    Console.WriteLine($"Сообщение с ошибкой {encoded.ToStringJoin()}");
            ////    //encoded = Sum_Vector(encoded, errorVector);
            ////    Console.WriteLine($"Исправленная сообщение: {encoded.ToStringJoin()}");
            ////}
            ////else if (Syndromes[syndrome] == 0)
            ////{
            ////    Console.WriteLine("Ошибка не найдена");
            ////}
            ////else
            ////{
            ////    Console.WriteLine("Восстановить сообщение без ошибок невозможно");
            ////}

            //Console.WriteLine($"{encoded.ToStringJoin()}");
            //var d = Decoding(encoded, Transpant_H);
            //Console.WriteLine(d.ToStringJoin());

            //Console.ReadKey();
        }

        public static List<int> Multiply_Matrix(List<int> str, int[,] Matrix)
        {
            var res = new List<int>();

            for (int i = 0; i < Matrix.GetLength(1); i++) //номер столбца
            {
                var sum = 0;

                for (int j = 0; j < Matrix.GetLength(0); j++) //номер строчки 
                {
                    sum += Matrix[j, i] * str[j];
                }

                res.Add(sum % 2);
            }

            return res;
        }

        //public static List<int> Sum_Vector(List<int> vec1, List<int> vec2)
        //{
        //    var res = new List<int>(vec1.Count);

        //    for (int i = 0; i < vec1.Count; i++)
        //    {
        //        res.Add((vec1[i] + vec2[i]) % 2);
        //    }

        //    return res;
        //}

        static List<int> Decoding(List<int> code)
        {
            //смежные классы:
            var A0 = new int[4, 5]
            {
                { 0, 0, 0, 0, 0 },
                { 1, 0, 1, 1, 1 },
                { 0, 1, 0, 1, 1 },
                { 1, 1, 1, 0, 0 }
            };

            var RelatedСlass = new List<int[,]>();

            var vectors = new int[7, 5]
            {
                { 1, 0, 0, 0, 0 },
                { 0, 1, 0, 0, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 0, 0, 1, 0 },
                { 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 1 },
                { 1, 0, 0, 1, 0 },
            };

            for (int i = 0; i < 7; i++)
            {
                var Ai = new int[4, 5];
                for (int j = 0; j < 4; j++)
                {
                    for (int r = 0; r < 5; r++)
                    {
                        Ai[j, r] = (vectors[i, r] + A0[j, r]) % 2;
                    }
                }

                RelatedСlass.Add(Ai);
            }

            //Console.WriteLine("Смежные классы не включая А0: ");
            //for (int i = 0; i < 7; i++)
            //{
            //    for (int j = 0; j < 4; j++)
            //    {
            //        for (int r = 0; r < 5; r++)
            //        {
            //            Console.Write($"{RelatedСlass[i][j, r]} ");
            //        }
            //        Console.WriteLine();
            //    }
            //    Console.WriteLine("\n");
            //}

            var syndrome = Multiply_Matrix(code, Transpant_H).ToStringJoin();

            Console.WriteLine($"Syndrome {syndrome}");

            if (Syndromes[syndrome] == 0)
            {
                Console.WriteLine("No error");
                return code;
            }

            var index = Syndromes[syndrome] - 1;

            var errorVector = Enumerable.Repeat(0, 5).ToList();
            errorVector[index] = 1;
            Console.WriteLine($"Error vector: {errorVector.ToStringJoin()}");

            var NumVector = 0;
            var minW = 6;

            for (int j = 0; j < 4; j++)
            {
                var w = 0;
                for (int r = 0; r < 5; r++)
                {
                    w += RelatedСlass[index][j, r];
                }

                if (minW > w)
                {
                    minW = w;
                    NumVector = j;
                }
            }

            var result = new List<int>();

            for (int i = 0; i < code.Count; i++)
            {
                result.Add(code[i] - RelatedСlass[index][NumVector, i]);
            }

            for (int i = 0; i < result.Count; i++)
            {
                if (result[i] < 0)
                    result[i] = 2 + result[i];
            }

            return result;
        }
    }
}