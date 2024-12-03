using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AoCWPF.Solutions
{
    /// <summary>
    /// Represents the solution for Day 3 of the Advent of Code challenge.
    /// </summary>
    public class Day3(int day, int part) : DayBase(day, part)
    {
        private int _day { get; set; } = day;
        private int _part { get; set; } = part;

        private string regPart1 = @"mul\((\d{1,3}),(\d{1,3})\)";
        private string regPart2 = @"mul\((\d{1,3}),(\d{1,3})\)|don't\(\)|do\(\)";

        /// <summary>
        /// Executes the logic for Part 1 of the challenge.
        /// </summary>
        /// <returns>The result of Part 1 as a string.</returns>
        public override string Part1()
        {
            var result = SolveRegex(RawInput, regPart1);
            Debug.WriteLine($"Result of Day {_day} Part {_part}: {result}");
            return result.ToString();
        }

        /// <summary>
        /// Executes the logic for Part 2 of the challenge.
        /// </summary>
        /// <returns>The result of Part 2 as a string.</returns>
        public override string Part2()
        {
            var result = SolveRegex(RawInput, regPart2);
            Debug.WriteLine($"Result of Day {_day} Part {_part}: {result}");
            return result.ToString();
        }

        /// <summary>
        /// Solves the challenge using the provided regular expression.
        /// </summary>
        /// <param name="input">The input string to process.</param>
        /// <param name="regex">The regular expression to use for matching.</param>
        /// <returns>The computed result as a long integer.</returns>
        public long SolveRegex(string input, string regex)
        {
            var matches = Regex.Matches(input, regex, RegexOptions.Multiline);
            long result = 0;
            bool enabled = true;

            foreach (Match match in matches)
            {
                switch (match.Value)
                {
                    case "don't()":
                        enabled = false;
                        break;
                    case "do()":
                        enabled = true;
                        break;
                    default:
                        if (enabled)
                        {
                            int num1 = int.Parse(match.Groups[1].Value);
                            int num2 = int.Parse(match.Groups[2].Value);
                            result += num1 * num2;
                        }
                        break;
                }
            }

            return result;
        }
    }
}
