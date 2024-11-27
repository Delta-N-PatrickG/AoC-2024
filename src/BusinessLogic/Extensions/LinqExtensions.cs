namespace Businesslogic.Extensions
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Retrieves every nth element from the list, starting from a specified offset.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list from which to retrieve elements.</param>
        /// <param name="n">The interval at which to retrieve elements.</param>
        /// <param name="offset">The starting offset for the retrieval. Default is 0.</param>
        /// <returns>A list containing every nth element from the original list, starting from the offset.</returns>
        public static List<T> GetNthElement<T>(this List<T> list, int n, int offset = 0)
        {
            return list.Select((item, index) => (index, item))
                       .Where(i => (i.index % n) - offset == 0)
                       .Select(i => i.item)
                       .ToList();
        }
    }
}