using Businesslogic.Locations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AoCWPF.Solutions
{
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

        public override string Part1()
        {
            var search = "XMAS";
            _grid = GetGrid();
            var result = CountAmountOfWord(_grid, search);
            Debug.WriteLine($"Result of Day {_day} Part {_part}: {result}");
            return result.ToString();
        }


        public override string Part2()
        {
            _grid = GetGrid();
            var result = MASCrossCount(_grid);
            Debug.WriteLine($"Result of Day {_day} Part {_part}: {result}");
            return result.ToString();
        }

        private List<List<string>> GetGrid()
        {
            return RawInput
             .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
             .Select(line => line.Select(c => c.ToString()).ToList())
             .ToList();
        }

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
