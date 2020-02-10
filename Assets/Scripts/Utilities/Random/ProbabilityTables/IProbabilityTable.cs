using System.Collections.Generic;

namespace SwordAndBored.Utilities.Random.ProbabilityTables
{
    /// <summary>
    /// A tool that provides a random item from a table of pre-weighted items
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IProbabilityTable<T>
    {
        /// <summary>
        /// Get the next random item from the pre-weighted table
        /// </summary>
        /// <returns>An item from the table</returns>
        T GetItem();

        /// <summary>
        /// Gets a number of items from the table, with possible repeats
        /// </summary>
        /// <returns>An enumerable of all the items returned</returns>
        IEnumerable<T> GetItems(int count);
    }
}
