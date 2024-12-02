using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Controls;

namespace AoCWPF.Solutions
{
    /// <summary>
    /// Represents the solution for Day 2 of the Advent of Code challenge.
    /// </summary>
    public class Day2(int day, int part) : DayBase(day, part)
    {
        private int _day { get; set; } = day;
        private int _part { get; set; } = part;
        private List<List<int>> _reports;

        /// <summary>
        /// Executes the logic for Part 1 of the challenge.
        /// </summary>
        /// <returns>The result of Part 1 as a string.</returns>
        public override string Part1()
        {
            _reports = GetReports();
            var result = GetSafeReports();
            Debug.WriteLine($"Result of Day {_day} Part {_part}: {result}");
            return result.ToString();
        }

        /// <summary>
        /// Executes the logic for Part 2 of the challenge.
        /// </summary>
        /// <returns>The result of Part 2 as a string.</returns>
        public override string Part2()
        {
            _reports = GetReports();
            var result = GetSafeReportsWithDampener();
            Debug.WriteLine($"Result of Day {_day} Part {_part}: {result}");
            return result.ToString();
        }

        /// <summary>
        /// Parses the raw input into a list of reports.
        /// </summary>
        /// <returns>A list of reports, where each report is a list of integers.</returns>
        private List<List<int>> GetReports()
        {
            return RawInput
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Split(' ').Select(int.Parse).ToList())
                .ToList();
        }

        /// <summary>
        /// Counts the number of safe reports.
        /// </summary>
        /// <returns>The number of safe reports.</returns>
        private int GetSafeReports()
        {
            var safeReports = 0;

            foreach (var report in _reports)
            {
                if (IsSafeReport(report)) safeReports++;
            }

            return safeReports;
        }

        /// <summary>
        /// Counts the number of safe reports, considering the Problem Dampener.
        /// </summary>
        /// <returns>The number of safe reports with the Problem Dampener.</returns>
        private int GetSafeReportsWithDampener()
        {
            var safeReports = 0;

            foreach (var report in _reports)
            {
                if (IsSafeReport(report) || IsSafeWithDampener(report)) safeReports++;
            }

            return safeReports;
        }

        /// <summary>
        /// Determines if a report is safe based on its readings.
        /// </summary>
        /// <param name="report">The report to check.</param>
        /// <returns>True if the report is safe, otherwise false.</returns>
        private bool IsSafeReport(List<int> report)
        {
            var increasing = report[0] <= report[1];
            return report.Zip(report.Skip(1), (current, next) => IsValidReading(current, next, increasing)).All(isSafe => isSafe);
        }

        /// <summary>
        /// Determines if a report is safe with the Problem Dampener by removing one level.
        /// </summary>
        /// <param name="report">The report to check.</param>
        /// <returns>True if the report is safe with one level removed, otherwise false.</returns>
        private bool IsSafeWithDampener(List<int> report)
        {
            return report
                .Select((value, index) => report.Where((innerValue, innerIndex) => innerIndex != index).ToList())
                .Any(IsSafeReport);
        }

        /// <summary>
        /// Validates a reading based on the current and next values and the trend.
        /// </summary>
        /// <param name="current">The current reading.</param>
        /// <param name="next">The next reading.</param>
        /// <param name="increasing">Indicates if the readings are increasing.</param>
        /// <returns>True if the reading is valid, otherwise false.</returns>
        private bool IsValidReading(int current, int next, bool increasing)
        {
            return Math.Abs(current - next) <= 3 && (!increasing || current < next) && (increasing || current > next);
        }
    }
}
