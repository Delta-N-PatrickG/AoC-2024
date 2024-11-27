using System.Diagnostics;

namespace Businesslogic.Locations
{
    [DebuggerDisplay("Coordinate = {Coord}, Value = {Value}")]
    public class CoordinateValue<T>(int x, int y, T value) : Coordinate(x, y)
    {
        public T Value { get; set; } = value;
    }
}