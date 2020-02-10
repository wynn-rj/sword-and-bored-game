using System.Collections.Generic;

namespace SwordAndBored.Utilities.Random.ProbabilityTables
{
    /// <summary>
    /// An IProbabilityTable where every item has an equal weight of being selected
    /// </summary>
    /// <typeparam name="T">The type of each item</typeparam>
    class EqualProbabilityTable<T> : IProbabilityTable<T>
    {
        private readonly List<T> table;

        public EqualProbabilityTable()
        {
            table = new List<T>();
        }

        public void AddItem(T item) => table.Add(item);

        public void AddItems(IEnumerable<T> items) => table.AddRange(items);

        public T GetItem() => Odds.SelectAtRandom(table.ToArray());
    }
}
