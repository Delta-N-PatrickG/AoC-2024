using System.Diagnostics;

namespace AoCWPF.Solutions
{
    /// <summary>
    /// Represents the solution for Day 6 of the Advent of Code challenge.
    /// </summary>
    public class Day6(int day, int part) : DayBase(day, part)
    {
        private int _day { get; set; } = day;

        private int _part { get; set; } = part;

        private List<List<string>> map { get; set; }

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
        /// Parses the raw input data into a map.
        /// </summary>
        private void GetData()
        {
            map = RawInput
             .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
             .Select(line => line.Select(c => c.ToString()).ToList())
             .ToList();
        }

        /// <summary>
        /// Solves part 1 of the challenge by moving the guard and counting visited positions.
        /// </summary>
        /// <returns>The number of visited positions.</returns>
        private int SolvePart1()
        {
            var rows = map.Count;
            var cols = map[0].Count;
            var guardRow = -1;
            var guardCol = -1;
            var direction = string.Empty;

            FindGuardStartingPosition(rows, cols, out guardRow, out guardCol, out direction);

            var visited = new HashSet<(int, int)>()
                {
                    (guardRow, guardCol)
                };

            MoveGuard(ref guardRow, ref guardCol, ref direction, ref visited);

            PrintMap(map, visited);
            return visited.Count();
        }

        /// <summary>
        /// Solves part 2 of the challenge by finding potential obstructions that create loops.
        /// </summary>
        /// <returns>The number of possible positions that create loops.</returns>
        private int SolvePart2()
        {
            var rows = map.Count;
            var cols = map[0].Count;
            var guardRow = -1;
            var guardCol = -1;
            var direction = string.Empty;

            FindGuardStartingPosition(rows, cols, out guardRow, out guardCol, out direction);

            var visited = new HashSet<(int, int)>()
                {
                    (guardRow, guardCol)
                };

            var possiblePositions = new List<(int, int)>();
            // Check all positions for potential obstructions
            foreach (var i in Enumerable.Range(0, rows))
            {
                foreach (var j in Enumerable.Range(0, cols))
                {
                    if (map[i][j] == "." && (i != guardRow || j != guardCol))
                    {
                        if (IsLoopCreated(i, j, guardRow, guardCol, direction))
                        {
                            possiblePositions.Add((i, j));
                        }
                    }
                }
            }

            return possiblePositions.Count;
        }

        /// <summary>
        /// Finds the guard's starting position and direction.
        /// </summary>
        /// <param name="rows">The number of rows in the map.</param>
        /// <param name="cols">The number of columns in the map.</param>
        /// <param name="guardRow">The row index of the guard's starting position.</param>
        /// <param name="guardCol">The column index of the guard's starting position.</param>
        /// <param name="direction">The initial direction the guard is facing.</param>
        private void FindGuardStartingPosition(int rows, int cols, out int guardRow, out int guardCol, out string direction)
        {
            guardRow = -1;
            guardCol = -1;
            direction = string.Empty;

            // Find the guard's starting position and direction
            foreach (var i in Enumerable.Range(0, rows))
            {
                foreach (var j in Enumerable.Range(0, cols))
                {
                    if (map[i][j] == "^" || map[i][j] == ">" || map[i][j] == "v" || map[i][j] == "<")
                    {
                        guardRow = i;
                        guardCol = j;
                        direction = map[i][j];
                        map[i][j] = "."; // Clear the starting position
                        break;
                    }
                }
                if (guardRow != -1) break;
            }
        }

        /// <summary>
        /// Moves the guard in the current direction and updates the visited positions.
        /// </summary>
        /// <param name="guardRow">The current row index of the guard.</param>
        /// <param name="guardCol">The current column index of the guard.</param>
        /// <param name="direction">The current direction the guard is facing.</param>
        /// <param name="visited">The set of visited positions.</param>
        private void MoveGuard(ref int guardRow, ref int guardCol, ref string direction, ref HashSet<(int, int)> visited)
        {
            var rows = map.Count;
            var cols = map[0].Count;
            var nextRow = guardRow;
            var nextCol = guardCol;

            switch (direction)
            {
                case "^":
                    nextRow--;
                    break;
                case ">":
                    nextCol++;
                    break;
                case "v":
                    nextRow++;
                    break;
                case "<":
                    nextCol--;
                    break;
            }

            if (nextRow < 0 || nextRow >= rows || nextCol < 0 || nextCol >= cols)
            {
                return;
            }

            if (map[nextRow][nextCol] == "#")
            {
                direction = TurnRight(direction);
            }
            else
            {
                guardRow = nextRow;
                guardCol = nextCol;
                visited.Add((guardRow, guardCol));
            }

            if (guardRow >= 0 && guardRow < rows && guardCol >= 0 && guardCol < cols)
            {
                MoveGuard(ref guardRow, ref guardCol, ref direction, ref visited);
            }
        }

        /// <summary>
        /// Turns the guard to the right.
        /// </summary>
        /// <param name="direction">The current direction the guard is facing.</param>
        /// <returns>The new direction after turning right.</returns>
        private string TurnRight(string direction)
        {
            return direction switch
            {
                "^" => ">",
                ">" => "v",
                "v" => "<",
                "<" => "^",
                _ => direction
            };
        }

        /// <summary>
        /// Prints the map with the visited positions marked.
        /// </summary>
        /// <param name="map">The map to print.</param>
        /// <param name="visited">The set of visited positions.</param>
        private void PrintMap(List<List<string>> map, HashSet<(int, int)> visited)
        {
            foreach (var i in Enumerable.Range(0, map.Count))
            {
                foreach (var j in Enumerable.Range(0, map[i].Count))
                {
                    if (visited.Contains((i, j)))
                        Debug.Write('X');
                    else
                        Debug.Write(map[i][j]);
                }
                Debug.WriteLine(string.Empty);
            }
        }

        /// <summary>
        /// Checks if placing an obstruction creates a loop.
        /// </summary>
        /// <param name="obstructionRow">The row index of the obstruction.</param>
        /// <param name="obstructionCol">The column index of the obstruction.</param>
        /// <param name="guardRow">The current row index of the guard.</param>
        /// <param name="guardCol">The current column index of the guard.</param>
        /// <param name="direction">The current direction the guard is facing.</param>
        /// <returns>True if a loop is created, otherwise false.</returns>
        private bool IsLoopCreated(int obstructionRow, int obstructionCol, int guardRow, int guardCol, string direction)
        {
            var visited = new HashSet<(int, int, string)>()
                {
                    (guardRow, guardCol, direction)
                };

            return CheckLoop(obstructionRow, obstructionCol, guardRow, guardCol, direction, visited);
        }

        /// <summary>
        /// Recursively checks for loops by simulating the guard's movement.
        /// </summary>
        /// <param name="obstructionRow">The row index of the obstruction.</param>
        /// <param name="obstructionCol">The column index of the obstruction.</param>
        /// <param name="guardRow">The current row index of the guard.</param>
        /// <param name="guardCol">The current column index of the guard.</param>
        /// <param name="direction">The current direction the guard is facing.</param>
        /// <param name="visited">The set of visited positions and directions.</param>
        /// <returns>True if a loop is detected, otherwise false.</returns>
        private bool CheckLoop(int obstructionRow, int obstructionCol, int guardRow, int guardCol, string direction, HashSet<(int, int, string)> visited)
        {
            var rows = map.Count;
            var cols = map[0].Count;
            var stack = new Stack<(int, int, string)>();
            stack.Push((guardRow, guardCol, direction));

            while (stack.Count > 0)
            {
                var (currentRow, currentCol, currentDirection) = stack.Pop();
                var nextRow = currentRow;
                var nextCol = currentCol;

                switch (currentDirection)
                {
                    case "^":
                        nextRow--;
                        break;
                    case ">":
                        nextCol++;
                        break;
                    case "v":
                        nextRow++;
                        break;
                    case "<":
                        nextCol--;
                        break;
                }

                if (nextRow < 0 || nextRow >= rows || nextCol < 0 || nextCol >= cols)
                {
                    continue;
                }

                if (nextRow == obstructionRow && nextCol == obstructionCol)
                {
                    currentDirection = TurnRight(currentDirection);
                }
                else if (map[nextRow][nextCol] == "#")
                {
                    currentDirection = TurnRight(currentDirection);
                }
                else
                {
                    currentRow = nextRow;
                    currentCol = nextCol;
                    if (visited.Contains((currentRow, currentCol, currentDirection)))
                    {
                        return true;
                    }
                    visited.Add((currentRow, currentCol, currentDirection));
                }

                stack.Push((currentRow, currentCol, currentDirection));
            }

            return false;
        }
    }
}
