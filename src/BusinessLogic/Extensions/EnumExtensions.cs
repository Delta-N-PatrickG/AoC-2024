namespace Businesslogic.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Retrieves a custom attribute of a specified type that is applied to a specified enum value.
        /// </summary>
        /// <typeparam name="T">The type of the attribute to retrieve.</typeparam>
        /// <param name="enumVal">The enum value to inspect.</param>
        /// <returns>The custom attribute of type T that is applied to the enum value, or null if no such attribute is found.</returns>
        public static T? GetAttributeOfType<T>(this Enum enumVal) where T : System.Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

        /// <summary>
        /// Splits an enumerable collection into batches of a specified maximum size.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the collection.</typeparam>
        /// <param name="items">The collection of items to batch.</param>
        /// <param name="maxItems">The maximum number of items in each batch.</param>
        /// <returns>An enumerable collection of batches, where each batch is an enumerable collection of items.</returns>
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> items,
                                                       int maxItems)
        {
            return items.Select((item, inx) => new { item, inx })
                        .GroupBy(x => x.inx / maxItems)
                        .Select(g => g.Select(x => x.item));
        }
    }
}