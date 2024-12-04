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

            //// top left = r - 1, c - 1
            //// top right = r - 1, c + 1
            //// Bottom left = r + 1, c - 1
            //// Bottom right = r + 1, c + 1


            // M top left, top right
            // S bottom left, bottom right
            if (IsValid(grid, r - 1, c - 1, "M") && // top left
                IsValid(grid, r - 1, c + 1, "M") && // top right
                IsValid(grid, r + 1, c - 1, "S") && // bottom left
                IsValid(grid, r + 1, c + 1, "S")) // bottom right
            {
                count++;
            }

            // M top left, bottom left
            // S top right, bottom right
            if (IsValid(grid, r - 1, c - 1, "M") && // top left
                IsValid(grid, r + 1, c - 1, "M") && // bottom left
                IsValid(grid, r - 1, c + 1, "S") && // top right
                IsValid(grid, r + 1, c + 1, "S")) // bottom right
            {
                count++;
            }

            // M bottom left, bottom right
            // S top left, top right
            if (IsValid(grid, r + 1, c - 1, "M") && // bottom left
                IsValid(grid, r + 1, c + 1, "M") && // bottom right
                IsValid(grid, r - 1, c - 1, "S") && // top left
                IsValid(grid, r - 1, c + 1, "S")) // top right
            {
                count++;
            }

            // M top right, bottom right
            // S top left, bottom left
            if (IsValid(grid, r - 1, c + 1, "M") && // top right
                IsValid(grid, r + 1, c + 1, "M") && // bottom right
                IsValid(grid, r - 1, c - 1, "S") && // top left
                IsValid(grid, r + 1, c - 1, "S")) // bottom left
            {
                count++;
            }

            return count;
        }

        private bool IsValid(List<List<string>> grid, int r, int c, string expected)
        { 
            var isValid = r >= 0 && r < grid.Count && c >= 0 && c < grid[0].Count && grid[r][c] == expected;
            if (!isValid)
            {
                Debug.WriteLine($"Invalid position or character at ({r}, {c}): expected '{expected}', found '{(r >= 0 && r < rows && c >= 0 && c < cols ? grid[r][c] : "out of bounds")}'");
            }
            return isValid;
        }
    }
}
