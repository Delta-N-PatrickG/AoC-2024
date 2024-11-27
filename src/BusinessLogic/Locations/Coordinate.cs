using System.Diagnostics;

namespace Businesslogic.Locations
{
    /// <summary>
    /// Represents a coordinate with X and Y values.
    /// </summary>
    [DebuggerDisplay("Coordinate = {Coord}")]
    public class Coordinate
    {
        /// <summary>
        /// Gets or sets the X value of the coordinate.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the Y value of the coordinate.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets the coordinate as a string in the format "X,Y".
        /// </summary>
        public string Coord => $"{X},{Y}";

        /// <summary>
        /// Initializes a new instance of the <see cref="Coordinate"/> class.
        /// </summary>
        public Coordinate()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Coordinate"/> class with specified X and Y values.
        /// </summary>
        /// <param name="x">The X value of the coordinate.</param>
        /// <param name="y">The Y value of the coordinate.</param>
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Parses a string to create a new <see cref="Coordinate"/> instance.
        /// </summary>
        /// <param name="input">The input string in the format "X,Y".</param>
        /// <returns>A new <see cref="Coordinate"/> instance.</returns>
        /// <exception cref="FormatException">Thrown when the input string is not in the correct format.</exception>
        public static Coordinate Parse(string input)
        {
            var xY = input
                .Split(",")
                .Select(i => Convert.ToInt32(i))
                .ToList();
            return new Coordinate(xY[0], xY[1]);
        }
    }
}