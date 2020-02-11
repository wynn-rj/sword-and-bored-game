using System;
using System.Collections.Generic;

namespace SwordAndBored.Utilities.Random.ProbabilityTables
{
    class WeightedProbabilityTable<T> : AbstractProbabilityTable<T>
    {
        private double weightSum;
        private List<Tuple<T, double>> weightedTable;
        public WeightedProbabilityTable()
        {
            weightSum = 0;
            weightedTable = new List<Tuple<T, double>>();
        }

        public void AddItem(T item, double weight)
        {
            weightedTable.Add(new Tuple<T, double>(item, weight));
        }
            
        public override T GetItem()
        {
            double pulledValue = Odds.PercentDouble() * weightSum;
            int index = -1;
            do {
                pulledValue -= weightedTable[++index].Item2;
            } while (pulledValue > 0);
            return weightedTable[index].Item1;
        }
    }
}
