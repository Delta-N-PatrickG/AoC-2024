namespace Businesslogic.Locations
{
    /// <summary>
    /// Represents a grid of elements of type T.
    /// </summary>
    /// <typeparam name="T">The type of elements in the grid.</typeparam>
    public class Grid<T>
    {
        /// <summary>
        /// Gets or sets the lines of the grid.
        /// </summary>
        public List<List<T>> Lines { get; set; }

        /// <summary>
        /// Gets the number of columns in the grid.
        /// </summary>
        public int SizeX { get; private set; }

        /// <summary>
        /// Gets the number of rows in the grid.
        /// </summary>
        public int SizeY { get; private set; }

        /// <summary>
        /// Gets all coordinates in the grid.
        /// </summary>
        public List<(int x, int y)> All { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid{T}"/> class.
        /// </summary>
        public Grid()
        {
            Lines = new List<List<T>>();
            All = new List<(int x, int y)>();
        }

        /// <summary>
        /// Initializes the grid with the specified size and value.
        /// </summary>
        /// <param name="sizeX">The number of columns.</param>
        /// <param name="sizeY">The number of rows.</param>
        /// <param name="value">The value to initialize each cell with.</param>
        public void Init(int sizeX, int sizeY, T value)
        {
            var init = Enumerable.Range(0, sizeY).Select(__ => Enumerable.Range(0, sizeX).Select(_ => value).ToList()).ToList();
            Parse(init);
        }

        /// <summary>
        /// Parses the input list to initialize the grid.
        /// </summary>
        /// <param name="input">The input list of lists to parse.</param>
        public void Parse(List<List<T>> input)
        {
            Lines = input;
            SizeX = Lines[0].Count;
            SizeY = Lines.Count;
            All = Enumerable.Range(0, SizeY).SelectMany(y => Enumerable.Range(0, SizeX).Select(x => (x, y))).ToList();
        }

        /// <summary>
        /// Checks if the specified coordinates exist in the grid.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>True if the coordinates exist, otherwise false.</returns>
        public bool Exists(int x, int y)
        {
            if (x < 0 || y < 0)
                return false;
            if (x > SizeX - 1 || y > SizeY - 1)
                return false;
            return true;
        }

        /// <summary>
        /// Updates the value at the specified coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="value">The new value.</param>
        public void UpdateValue(int x, int y, T value)
        {
            Lines[y][x] = value;
        }

        /// <summary>
        /// Updates the value at the specified coordinates using a function.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="update">The function to update the value.</param>
        public void UpdateValue(int x, int y, Func<T, T> update)
        {
            Lines[y][x] = update(Lines[y][x]);
        }

        /// <summary>
        /// Gets the value at the specified coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>The value at the specified coordinates.</returns>
        public T GetValue(int x, int y)
        {
            return Lines[y][x];
        }

        /// <summary>
        /// Gets the coordinates that match the specified filter.
        /// </summary>
        /// <param name="filter">The filter function.</param>
        /// <returns>A list of coordinates that match the filter.</returns>
        public List<(int x, int y)> GetCoordinatesFiltered(Func<T, bool> filter)
        {
            return All.Where(i => filter(Lines[i.y][i.x])).ToList();
        }

        /// <summary>
        /// Gets the adjoining coordinates and values in all directions.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>A list of adjoining coordinates and values.</returns>
        public List<CoordinateValue<T>> GetAdjoiningAll(int x, int y)
        {
            return new[] { GetCoordinateValue(x, y - 1), // Top
                               GetCoordinateValue(x + 1, y - 1), // Top Right
                               GetCoordinateValue(x + 1, y), // Right
                               GetCoordinateValue(x + 1, y + 1), // Bottom Right
                               GetCoordinateValue(x, y + 1),  // Bottom
                               GetCoordinateValue(x - 1, y + 1), // Bottom Left
                               GetCoordinateValue(x - 1, y), // Left
                               GetCoordinateValue(x - 1, y - 1) // Top Left
                             }.Where(i => i != null)
                          .Select(i => i!)
                          .ToList();
        }

        /// <summary>
        /// Gets the adjoining coordinates and values in cross directions.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>A list of adjoining coordinates and values.</returns>
        public List<CoordinateValue<T>> GetAdjoiningCross(int x, int y)
        {
            return new[] { GetCoordinateValue(x, y - 1), // Top
                               GetCoordinateValue(x + 1, y), // Right
                               GetCoordinateValue(x, y + 1),  // Bottom
                               GetCoordinateValue(x - 1, y), // Left
                             }.Where(i => i != null)
                         .Select(i => i!)
                         .ToList();
        }

        /// <summary>
        /// Gets the values in the specified row.
        /// </summary>
        /// <param name="y">The row index.</param>
        /// <returns>A list of coordinate values in the row.</returns>
        public List<CoordinateValue<T>> GetRow(int y)
        {
            return Enumerable.Range(0, SizeX)
                .Select(x => GetCoordinateValue(x, y))
                .Where(cv => cv != null)
                .Select(cv => cv!)
                .ToList();
        }

        /// <summary>
        /// Gets the values in the specified column.
        /// </summary>
        /// <param name="x">The column index.</param>
        /// <returns>A list of coordinate values in the column.</returns>
        public List<CoordinateValue<T>> GetColum(int x)
        {
            return Enumerable.Range(0, SizeY)
                .Select(y => GetCoordinateValue(x, y))
                .Where(cv => cv != null)
                .Select(cv => cv!)
                .ToList();
        }

        /// <summary>
        /// Gets the coordinate value at the specified coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>The coordinate value at the specified coordinates, or null if the coordinates do not exist.</returns>
        public CoordinateValue<T>? GetCoordinateValue(int x, int y)
        {
            if (!Exists(x, y))
                return null;
            return new CoordinateValue<T>(x, y, Lines[y][x]);
        }
    }
}