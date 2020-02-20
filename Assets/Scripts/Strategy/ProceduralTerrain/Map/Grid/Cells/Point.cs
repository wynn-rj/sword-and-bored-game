namespace SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells
{
    /// <summary>
    /// A 2D Point with an X and Y coordinate, where the coordinates are of type T
    /// </summary>
    /// <typeparam name="T">The type for the coordinate</typeparam>
    public class Point<T>
    {
        public T Y { get; }
        
        public T X { get; }

        public Point(T x, T y)
        {
            X = x;
            Y = y;
        }
    } 
}