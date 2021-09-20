using System;
using System.Collections.Generic;
using System.Linq;

namespace techswitch_fizz_buzz.cs
{

    class FizzBuzz
    {
        static string[] YES = new string[] { "y", "Y", "Yes", "yes", "yeah", "Yeah" };
        static int[] SPECIAL_RULES = new int[] { 11, 13, 17 };
        static Dictionary<int, string> RULE_DICT = new Dictionary<int, string>()
        {
            {3,"Fizz"},
            {5, "Buzz"},
            {7,"Bang"},
            {11,"Bong"},
            {13, "Fezz"},
            {17,"reverse"},
        };
        static bool IsDivisible(int number, int factor) => number % factor == 0;
        static int GetNumber(string message)
        {
            Console.WriteLine(message);
            return Convert.ToInt32(Console.ReadLine());
        }
        static IEnumerable<int> GetRules()
        {
            Console.WriteLine("Would you like to use all of the rules? y/n");

            if (YES.Contains(Console.ReadLine()))
            {
                return RULE_DICT.Keys;
            }

            Console.WriteLine("Please select rules via their number:");
            Console.WriteLine(string.Join(Environment.NewLine, RULE_DICT));
            Console.WriteLine("Type 'stop' when you're finished");

            List<int> rules = new List<int>();

            while (rules.Count < RULE_DICT.Count)
            {
                string rule = Console.ReadLine();

                if (rule.Equals("stop"))
                {
                    if (rules.Count == 0)
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

                    if (int.TryParse(rule, out ruleNumber) && RULE_DICT.ContainsKey(ruleNumber) && !rules.Contains(ruleNumber))
                    {
                        rules.Add(ruleNumber);
                    }
                    else
                    {
                        Console.WriteLine("Not a valid command.");
                    }
                }
            }

            return rules.AsEnumerable();

        }

        static string GetFizzBuzzString(int number, IEnumerable<int> rules)
        {
            List<string> result = new List<string>();

            if (rules.Contains(11) && IsDivisible(number, 11))
            {
                rules = rules.Where(x => SPECIAL_RULES.Contains(x));
            }

            if (rules.Contains(17) && IsDivisible(number, 17))
            {
                rules = rules.Take(rules.Count() - 1).Reverse();
            }

            foreach (int rule in rules)
            {
                if (IsDivisible(number, rule))
                {
                    if (rule == 13)
                    {
                        int indexOfFirstB = result.FindIndex(word => word[0].Equals('B'));
                        int index = indexOfFirstB == -1 ? 0 : indexOfFirstB;
                        result.Insert(index, RULE_DICT[rule]);
                    }
                    else
                    {
                        result.Add(RULE_DICT[rule]);
                    }
                }
            }
            return result.Count() == 0 ? number.ToString() : String.Join("", result);
        }

        static void RunFizzBuzz(int start, int end, IEnumerable<int> rules)
        {
            for (int i = start; i < end; i++)
            {
                Console.WriteLine(GetFizzBuzzString(i, rules));

            }
        }

        static void Main(string[] args)
        {
            int start = GetNumber("Start number:");
            int end = GetNumber("End number:");
            IEnumerable<int> rules = GetRules();
            RunFizzBuzz(start, end, rules);
        }
    }
}
