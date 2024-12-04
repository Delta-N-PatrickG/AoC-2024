using System.Diagnostics;

namespace AoCWPF.Solutions
{
    /// <summary>
    /// Represents the solution for Day 4 of the Advent of Code challenge.
    /// </summary>
    public class Day4(int day, int part) : DayBase(day, part)
    {
        private int _day { get; set; } = day;
        private int _part { get; set; } = part;
        private List<List<string>> _grid;

        private int[][] _possibleDirections = {
                    [0, 1],  // Right
                    [1, 0],  // Down
                    [1, 1],  // Down-Right
                    [1, -1], // Down-Left
                    [0, -1], // Left
                    [-1, 0], // Up
                    [-1, 1], // Up-Right
                    [-1, -1] // Up-Left
                };

        /// <summary>
        /// Solves part 1 of the challenge.
        /// </summary>
        /// <returns>The result of part 1 as a string.</returns>
        public override string Part1()
        {
            var search = "XMAS";
            _grid = GetGrid();
            var result = CountAmountOfWord(_grid, search);
            Debug.WriteLine($"Result of Day {_day} Part {_part}: {result}");
            return result.ToString();
        }

        /// <summary>
        /// Solves part 2 of the challenge.
        /// </summary>
        /// <returns>The result of part 2 as a string.</returns>
        public override string Part2()
        {
            _grid = GetGrid();
            var result = MASCrossCount(_grid);
            Debug.WriteLine($"Result of Day {_day} Part {_part}: {result}");
            return result.ToString();
        }

        /// <summary>
        /// Parses the raw input into a grid of characters.
        /// </summary>
        /// <returns>A grid of characters as a list of lists of strings.</returns>
        private List<List<string>> GetGrid()
        {
            return RawInput
             .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
             .Select(line => line.Select(c => c.ToString()).ToList())
             .ToList();
        }

        /// <summary>
        /// Counts the number of times a specific word appears in the grid in any direction.
        /// </summary>
        /// <param name="grid">The grid of characters to search within.</param>
        /// <param name="search">The word to search for in the grid.</param>
        /// <returns>The count of the word found in the grid.</returns>
        private int CountAmountOfWord(List<List<string>> grid, string search)
        {
            var count = 0;

            foreach (var row in Enumerable.Range(0, grid.Count))
            {
                foreach (var col in Enumerable.Range(0, grid[0].Count))
                {
                    if (grid[row][col] == search[0].ToString())
                    {
                        count += CheckAllDirections(grid, search, row, col);
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// Checks all possible directions from a starting point in the grid for a specific word.
        /// </summary>
        /// <param name="grid">The grid of characters to search within.</param>
        /// <param name="search">The word to search for in the grid.</param>
        /// <param name="row">The starting row index.</param>
        /// <param name="col">The starting column index.</param>
        /// <returns>The count of the word found in all directions from the starting point.</returns>
        private int CheckAllDirections(List<List<string>> grid, string search, int row, int col)
        {
            var count = 0;

            foreach (var dir in _possibleDirections)
            {
                var newRow = row;
                var newCol = col;
                var counter = 0;
                foreach (var i in Enumerable.Range(0, search.Length))
                {
                    if (newRow < 0 || newRow >= grid.Count || newCol < 0 || newCol >= grid[0].Count || grid[newRow][newCol] != search[i].ToString())
                    {
                        continue;
                    }
                    newRow += dir[0];
                    newCol += dir[1];
                    counter++;
                }
                if (counter == search.Length)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Counts the number of "MAS" crosses in the grid.
        /// </summary>
        /// <param name="grid">The grid of characters to search within.</param>
        /// <returns>The count of "MAS" crosses found in the grid.</returns>
        private int MASCrossCount(List<List<string>> grid)
        {
            var count = 0;

            foreach (var row in Enumerable.Range(0, grid.Count))
            {
                foreach (var col in Enumerable.Range(0, grid[0].Count))
                {
                    if (grid[row][col] == "A")
                    {
                        count += CheckDirections(grid, row, col);
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// Checks the surrounding positions for "M" and "S" characters to form a "MAS" cross.
        /// </summary>
        /// <param name="grid">The grid of characters to search within.</param>
        /// <param name="r">The row index of the "A" character.</param>
        /// <param name="c">The column index of the "A" character.</param>
        /// <returns>The count of valid "MAS" crosses found.</returns>
        private int CheckDirections(List<List<string>> grid, int r, int c)
        {
            var count = 0;

            // Define the positions to check for 'M' and 'S'
            var positions = new (int, int, string)[]
            {
                    (r - 1, c - 1, "M"), (r - 1, c + 1, "M"), (r + 1, c - 1, "S"), (r + 1, c + 1, "S"),
                    (r - 1, c - 1, "M"), (r + 1, c - 1, "M"), (r - 1, c + 1, "S"), (r + 1, c + 1, "S"),
                    (r + 1, c - 1, "M"), (r + 1, c + 1, "M"), (r - 1, c - 1, "S"), (r - 1, c + 1, "S"),
                    (r - 1, c + 1, "M"), (r + 1, c + 1, "M"), (r - 1, c - 1, "S"), (r + 1, c - 1, "S")
            };

            // Check the positions in groups of four
            for (int i = 0; i < positions.Length; i += 4)
            {
                if (IsValid(grid, positions[i].Item1, positions[i].Item2, positions[i].Item3) &&
                    IsValid(grid, positions[i + 1].Item1, positions[i + 1].Item2, positions[i + 1].Item3) &&
                    IsValid(grid, positions[i + 2].Item1, positions[i + 2].Item2, positions[i + 2].Item3) &&
                    IsValid(grid, positions[i + 3].Item1, positions[i + 3].Item2, positions[i + 3].Item3))
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Validates if a position in the grid contains the expected character.
        /// </summary>
        /// <param name="grid">The grid of characters to search within.</param>
        /// <param name="r">The row index to check.</param>
        /// <param name="c">The column index to check.</param>
        /// <param name="expected">The expected character at the position.</param>
        /// <returns>True if the position is valid and contains the expected character, otherwise false.</returns>
        private bool IsValid(List<List<string>> grid, int r, int c, string expected)
        {
            var isValid = r >= 0 && r < grid.Count && c >= 0 && c < grid[0].Count && grid[r][c] == expected;
            if (!isValid)
            {
                Debug.WriteLine($"Invalid position or character at ({r}, {c}): expected '{expected}', found '{(r >= 0 && r < grid.Count && c >= 0 && c < grid[0].Count ? grid[r][c] : "out of bounds")}'");
            }
            return isValid;
        }
    }
}
