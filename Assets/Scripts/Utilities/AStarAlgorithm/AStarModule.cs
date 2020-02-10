using System.Collections.Generic;
using System;

namespace SwordAndBored.Utilities.AStarAlgorithm
{
    public class AStarModule<T>
    {
        private T[][] grid;

        public AStarModule(T[][] grid)
        {
            this.grid = grid;
        }

        public List<T> findPath(Tuple<int, int> startPoint, Tuple<int, int> destination)
        {
            T dest = grid[destination.Item1][destination.Item2];

            return new List<T>();
        }

        private struct Node
        {
            public T Data { get; set; }
            public Tuple<int, int> Location { get; set; }
            public T Parent { get; set; }
        }
    }
}
