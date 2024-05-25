using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line_Code__5_2_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] G = new int[2, 5] { { 1, 0, 1, 0, 1 }, { 0, 1, 0, 1, 1 } };
            Console.WriteLine("Введите кодовое слово:");
            string input = Console.ReadLine();
            List<int> Int_Input = new List<int>() { input[0] - '0', input[1] - '0' };
            List<int> Encoded_Input = Multiply_Matrix(Int_Input, G);
            Console.WriteLine("Закодированное сообщение:");
            List<int> Encoded_Check = new List<int>(Encoded_Input);
            foreach (int i in Encoded_Input)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Введите индекс ошибки:");
            int Index_Of_Mistake = int.Parse(Console.ReadLine()) - 1;
            if (Index_Of_Mistake != -1)
            {
                Encoded_Input[Index_Of_Mistake] = (Encoded_Input[Index_Of_Mistake] + 1) % 2;
            }
            int[,] Transpant_H = new int[5, 3] { { 1, 0, 1 }, { 0, 1, 1 }, { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
            List<int> Syndrome = Multiply_Matrix(Encoded_Input, Transpant_H);
            Dictionary<string, int> Syndromes = new Dictionary<string, int>
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
            string Str_Syn = string.Join("", Syndrome.ToArray());
            string errorVector = "";
            for (int i = 1; i < 6; i++)
            {
                if (i == Syndromes[Str_Syn])
                {
                    errorVector += "1";
                }
                else
                {
                    errorVector += "0";
                }
            }
            if (Syndromes[Str_Syn] > 0)
            {
                Encoded_Input[Syndromes[Str_Syn] - 1] = (Encoded_Input[Syndromes[Str_Syn] - 1] + 1) % 2;
                Console.WriteLine($"Синдром: {Str_Syn}");
                Console.WriteLine($"Вектор ошибки: {errorVector}");
                Console.WriteLine($"Исправленная строка: {string.Join("", Encoded_Input.ToArray())}");
            }
            else if (Syndromes[Str_Syn] == 0)
            {
                Console.WriteLine("Ошибка не совершалась");
            }
            else
            {
                Console.WriteLine("Восстановить сообщение без ошибок невозможно");
            }
            Console.ReadKey();
        }
        public static List<int> Multiply_Matrix(List<int> str, int[,] Matrix)
        {
            List<int> Answer = new List<int>();
            //Умножение
            for (int i = 0; i < Matrix.GetLength(1); i++)//номер столбца
            {
                int sum = 0;
                for (int j = 0; j < Matrix.GetLength(0); j++)//номер строчки 
                {
                    sum += (Matrix[j, i] * (str[j]));
                }
                Answer.Add(sum % 2);
            }
            return Answer;
        }
    }
}