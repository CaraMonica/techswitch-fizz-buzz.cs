using System;
using System.Collections.Generic;
using System.Linq;

namespace techswitch_fizz_buzz.cs
{

    class Program
    {

        static void FizzBuzz1()
        {
            for (int i = 1; i <= 100; i++)
            {
                if (!IsDivisible(i, 3) && !IsDivisible(i, 5))
                {
                    Console.WriteLine(i);
                    continue;
                }
                else if (IsDivisible(i, 3))
                {
                    Console.WriteLine("Fizz");
                    continue;
                }
                else if (IsDivisible(i, 5))
                {
                    Console.WriteLine("Buzz");
                    continue;
                }
                else
                {
                    Console.WriteLine("FizzBuzz");
                }
            }
        }

        static Dictionary<int, string> ruleDict = new Dictionary<int, string>()
        {
            {3,"Fizz"},
            {5, "Buzz"},
            {7,"Bang"},
            {11,"Bong"},
            {13, "Fezz"},
            {17,"reverse"},
        };

        static bool IsDivisible(int number, int factor) => number % factor == 0;

        static string GetFizzBuzzString(int number)
        {
            List<int> result = new List<int>();
            return String.Join("", result);
        }

        static IEnumerable<int> FizzBuzz(int start, int end)
        {
            return Enumerable.Range(start, end).Select(x => x);
        }

        static void Main(string[] args)
        {
            var lines = ruleDict.Select(kvp => kvp.Key + ": " + kvp.Value.ToString());
            Console.WriteLine(String.Join(Environment.NewLine, lines));
        }
    }
}
