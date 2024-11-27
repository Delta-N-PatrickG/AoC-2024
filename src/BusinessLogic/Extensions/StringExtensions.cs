namespace Businesslogic.Extensions
{
    /// <summary>
    /// Provides extension methods for string manipulation.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Determines whether the string contains any of the specified characters.
        /// </summary>
        /// <param name="self">The string to check.</param>
        /// <param name="items">The characters to check for.</param>
        /// <returns>True if the string contains any of the specified characters; otherwise, false.</returns>
        public static bool ContainsAny(this string self, IEnumerable<char> items)
        {
            return self.ToArray().Any(x => items.Contains(x));
        }

        /// <summary>
        /// Splits the string into chunks of the specified size.
        /// </summary>
        /// <param name="str">The string to split.</param>
        /// <param name="chunkSize">The size of each chunk.</param>
        /// <returns>An enumerable collection of string chunks.</returns>
        public static IEnumerable<string> Split(this string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }

        /// <summary>
        /// Determines whether the string represents a numeric value.
        /// </summary>
        /// <param name="str">The string to check.</param>
        /// <returns>True if the string represents a numeric value; otherwise, false.</returns>
        public static bool IsNumeric(this string str)
        {
            return int.TryParse(str, out _);
        }
    }
}