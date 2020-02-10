using System.Collections.Generic;

namespace SwordAndBored.Utilities.Random.ProbabilityTables
{
    abstract class AbstractProbabilityTable<T> : IProbabilityTable<T>
    {
        public abstract T GetItem();

        public IEnumerable<T> GetItems(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return GetItem();
            }
        }
    }
}
