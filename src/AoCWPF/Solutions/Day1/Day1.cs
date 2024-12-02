using System.Diagnostics;
using System.IO;
using System.Reflection;
using BusinessLogic;

namespace AoCWPF.Solutions
{
    /// <summary>
    /// Solution for Day 1 of the Advent of Code challenge.
    /// </summary>
    public class Day1
    {
        private LocationData _locationData { get; set; }
        private List<string> Input { get; set; }
        private string RawInput { get; set; }

        /// <summary>
        /// Solves Part 1 of Day 1.
        /// </summary>
        /// <returns>The result of the calculation for Part 1.</returns>
        public int Part1()
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"InputFiles\Day1\part1.txt");
            Helper.GetFileData(path, out var input, out var rawInput);
            Input = input;
            RawInput = rawInput;
            _locationData = ParseLocationData();
            var result = GetDifferenceSum();
            Debug.WriteLine($"Result of Day 1 Part 1: {result}");
            return result;
        }

        /// <summary>
        /// Solves Part 2 of Day 1.
        /// </summary>
        /// <returns>The result of the calculation for Part 2.</returns>
        public int Part2()
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"InputFiles\Day1\part2.txt");
            Helper.GetFileData(path, out var input, out var rawInput);
            Input = input;
            RawInput = rawInput;
            _locationData = ParseLocationData();
            var result = CalculateSimilarityScore();
            Debug.WriteLine($"Result of Day 1 Part 2: {result}");
            return result;
        }

        /// <summary>
        /// Parses the location data from the input.
        /// </summary>
        /// <returns>A LocationData object containing the parsed data.</returns>
        private LocationData ParseLocationData()
        {
            var locationContainer = new LocationData(Input.Count);

            foreach (var line in RawInput.AsSpan().EnumerateLines())
            {
                var (id1, id2) = ParseLine(line);
                locationContainer.One.Add(id1);
                locationContainer.Two.Add(id2);
            }

            locationContainer.One.Sort();
            locationContainer.Two.Sort();

            return locationContainer;
        }

        /// <summary>
        /// Parses a line of input into two integers.
        /// </summary>
        /// <param name="line">The line of input to parse.</param>
        /// <returns>A tuple containing the two parsed integers.</returns>
        private (int, int) ParseLine(ReadOnlySpan<char> line)
        {
            var spaceIndex = line.IndexOf(' ');
            var id1 = int.Parse(line.Slice(0, spaceIndex));
            var id2 = OptimalParseInt(line.Slice(spaceIndex + 1));
            return (id1, id2);
        }

        /// <summary>
        /// Calculates the sum of differences between corresponding elements in the two lists.
        /// </summary>
        /// <returns>The sum of differences.</returns>
        private int GetDifferenceSum()
        {
            var difference = 0;
            var one = _locationData.One;
            var two = _locationData.Two;

            for (var i = 0; i < one.Count; i++)
            {
                difference += Math.Abs(one[i] - two[i]);
            }

            return difference;
        }

        /// <summary>
        /// Calculates the similarity score based on the frequency of elements in the second list.
        /// </summary>
        /// <returns>The similarity score.</returns>
        private int CalculateSimilarityScore()
        {
            var score = 0;
            var one = _locationData.One;
            var two = _locationData.Two;

            var groupedTwo = two.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());

            foreach (var value in one)
            {
                if (groupedTwo.TryGetValue(value, out var frequency))
                {
                    score += value * frequency;
                }
            }

            return score;
        }

        /// <summary>
        /// Parses a ReadOnlySpan of characters to an integer, ignoring non-digit characters. 
        /// </summary>
        /// <param name="input">The input ReadOnlySpan to parse.</param>
        /// <returns>The parsed integer value.</returns>
        private int OptimalParseInt(ReadOnlySpan<char> input)
        {
            var value = 0;
            foreach (var i in input)
            {
                if (i < '0' || i > '9')
                {
                    continue;
                }
                // see Explanation.txt in the same folder for more information on this calculation
                value = value * 10 + (i - '0');
            }

            return value;
        }

        /// <summary>
        /// Container for location data.
        /// </summary>
        private class LocationData()
        {
            /// <summary>
            /// Initializes a new instance of the LocationData class with the specified length.
            /// </summary>
            /// <param name="length">The initial length of the lists.</param>
            public LocationData(int length) : this()
            {
                One = new List<int>();
                Two = new List<int>();
            }

            public List<int> One { get; }
            public List<int> Two { get; }
        }
    }
}
