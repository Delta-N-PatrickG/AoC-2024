using System.Data;
using System.Diagnostics;

namespace AoCWPF.Solutions
{
    /// <summary>
    /// Represents the solution for Day 5 of the Advent of Code challenge.
    /// </summary>
    public class Day5(int day, int part) : DayBase(day, part)
    {
        private int _day { get; set; } = day;

        private int _part { get; set; } = part;

        private List<(int, int)> _rules { get; set; }

        private List<List<int>> _updates { get; set; }

        /// <summary>
        /// Solves part 1 of the challenge.
        /// </summary>
        /// <returns>The result of part 1 as a string.</returns>
        public override string Part1()
        {
            GetData();
            var result = SolvePart1();
            Debug.WriteLine($"Result of Day {_day} Part {_part}: {result}");
            return result.ToString();
        }

        /// <summary>
        /// Solves part 2 of the challenge.
        /// </summary>
        /// <returns>The result of part 2 as a string.</returns>
        public override string Part2()
        {
            GetData();
            var result = SolvePart2();
            Debug.WriteLine($"Result of Day {_day} Part {_part}: {result}");
            return result.ToString();
        }

        /// <summary>
        /// Retrieves and processes the input data for the challenge.
        /// </summary>
        private void GetData()
        {
            var sections = RawInput.Split(new[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.None);
            _rules = sections[0].Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Split("|"))
                .Select(parts => (int.Parse(parts[0]), int.Parse(parts[1])))
                .ToList();

            _updates = sections[1].Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Split(',').Select(int.Parse).ToList())
                .ToList();
        }

        /// <summary>
        /// Solves part 1 of the challenge by calculating the sum of the middle page numbers of correctly ordered updates.
        /// </summary>
        /// <returns>The sum of the middle page numbers.</returns>
        private int SolvePart1()
        {
            var correctUpdatesMiddlePages = new List<int>();
            foreach (var update in _updates)
            {
                if (IsCorrectOrder(update))
                {
                    correctUpdatesMiddlePages.Add(MiddlePage(update));
                }
            }
            var result = correctUpdatesMiddlePages.Sum();
            Debug.WriteLine($"The sum of the middle page numbers is {result}.");

            return result;
        }

        /// <summary>
        /// Solves part 2 of the challenge by reordering incorrectly ordered updates and calculating the sum of the middle page numbers.
        /// </summary>
        /// <returns>The sum of the middle page numbers.</returns>
        private int SolvePart2()
        {
            var correctUpdatesMiddlePages = new List<int>();
            foreach (var update in _updates)
            {
                if (!IsCorrectOrder(update))
                {
                    var sortedList = ReorderUpdate(update);
                    correctUpdatesMiddlePages.Add(MiddlePage(sortedList));
                }
            }
            var result = correctUpdatesMiddlePages.Sum();
            Debug.WriteLine($"The sum of the middle page numbers is {result}.");

            return result;
        }

        /// <summary>
        /// Determines if the given update is in the correct order based on the rules.
        /// </summary>
        /// <param name="update">The update to check.</param>
        /// <returns>True if the update is in the correct order; otherwise, false.</returns>
        private bool IsCorrectOrder(List<int> update)
        {
            foreach (var rule in _rules)
            {
                var x = rule.Item1;
                var y = rule.Item2;
                if (!update.Contains(x) || !update.Contains(y))
                {
                    continue;
                }
                if (update.IndexOf(x) > update.IndexOf(y))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Reorders the given update based on the rules.
        /// </summary>
        /// <param name="update">The update to reorder.</param>
        /// <returns>The reordered update.</returns>
        private List<int> ReorderUpdate(List<int> update)
        {
            var newList = new List<int>(update);

            foreach (var rule in _rules)
            {
                var x = rule.Item1;
                var y = rule.Item2;

                if (newList.Contains(x) && newList.Contains(y))
                {
                    var indexX = newList.IndexOf(x);
                    var indexY = newList.IndexOf(y);

                    if (indexX > indexY)
                    {
                        newList.RemoveAt(indexX);
                        newList.Insert(indexY, x);
                        return ReorderUpdate(newList); 
                    }
                }
            }

            return newList;
        }

        /// <summary>
        /// Gets the middle page number from the given update.
        /// </summary>
        /// <param name="update">The update to get the middle page number from.</param>
        /// <returns>The middle page number.</returns>
        private int MiddlePage(List<int> update)
        {
            return update[update.Count / 2];
        }
    }
}
