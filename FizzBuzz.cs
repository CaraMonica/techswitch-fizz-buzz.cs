using System;
using System.Collections.Generic;
using System.Linq;

namespace techswitch_fizz_buzz.cs
{
    class FizzBuzz
    {

        static string[] YES = new string[] { "y", "Y", "Yes", "yes", "yeah", "Yeah" };
        static Dictionary<int, string> WORD_BY_FACTOR = new Dictionary<int, string>()
        {
            {3,"Fizz"},
            {5, "Buzz"},
            {7,"Bang"},
            {11,"Bong"},
            {13, "Fezz"},
            {17,"Reverse"},
        };
        static List<int> SPECIAL_FACTORS = new List<int> { 11, 17, 13 };

        static List<int> ApplyBong(List<int> factors) => factors.Where(SPECIAL_FACTORS.Contains).ToList();
        static List<int> ApplyReverse(List<int> factors)
        {
            List<int> factorsCopy = new List<int>(factors);
            factorsCopy.Remove(17);
            return factorsCopy.Where(SPECIAL_FACTORS.Contains).ToList();
        }
        static List<int> ApplyFezz(List<int> factors)
        {
            List<int> factorsCopy = new List<int>(factors);
            factorsCopy.Remove(13);
            int index = WORD_BY_FACTOR[factorsCopy[0]][0].Equals('F') ? 1 : 0;
            factorsCopy.Insert(index, 13);
            return factorsCopy;
        }
        static Dictionary<int, Func<List<int>, List<int>>> FUNCTION_BY_FACTOR = new Dictionary<int, Func<List<int>, List<int>>>()
        {
            {11, ApplyBong},
            {17, ApplyReverse},
            {13, ApplyFezz},
        };

        static bool IsDivisible(int n, int factor) => n % factor == 0;
        static bool ShouldApplyRule(int n, List<int> factors, int factor) => factors.Contains(factor) && IsDivisible(n, factor);

        static int GetNumber(string message)
        {
            Console.WriteLine(message);
            return Convert.ToInt32(Console.ReadLine());
        }

        static List<int> GetFactors()
        {
            Console.WriteLine("Would you like to use all of the rules? y/n");

            if (YES.Contains(Console.ReadLine()))
            {
                return WORD_BY_FACTOR.Keys.ToList();
            }

            Console.WriteLine("Please select rules via their number:");
            Console.WriteLine(string.Join(Environment.NewLine, WORD_BY_FACTOR));
            Console.WriteLine("Type 'stop' when you're finished");

            List<int> factors = new List<int>();

            while (factors.Count < WORD_BY_FACTOR.Count)
            {
                string rule = Console.ReadLine();
                if (rule.ToLower().Equals("stop"))
                {
                    if (factors.Count == 0)
                    {
                        Console.WriteLine("Must have at least one rule.");
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    int ruleNumber;
                    if (int.TryParse(rule, out ruleNumber) && WORD_BY_FACTOR.ContainsKey(ruleNumber) && !factors.Contains(ruleNumber))
                    {
                        factors.Add(ruleNumber);
                    }
                    else
                    {
                        Console.WriteLine("Not a valid command.");
                    }
                }
            }

            return factors.OrderBy(i => i).ToList();
        }

        static string GetFizzBuzzString(int n, List<int> factors)
        {
            foreach (int f in SPECIAL_FACTORS)
            {
                factors = ShouldApplyRule(n, factors, f) ? FUNCTION_BY_FACTOR[f](factors) : factors;
            }

            IEnumerable<string> result = factors.Where(f => IsDivisible(n, f)).Select(f => WORD_BY_FACTOR[f]);
            return result.Count() == 0 ? n.ToString() : String.Join("", result);
        }

        static void RunFizzBuzz(int start, int end, List<int> factors)
        {
            for (int i = start; i <= end; i++)
            {
                Console.WriteLine(GetFizzBuzzString(i, factors));
            }
        }

        static void Main(string[] args)
        {
            RunFizzBuzz(GetNumber("Start number:"), GetNumber("End number:"), GetFactors());
        }
    }
}
