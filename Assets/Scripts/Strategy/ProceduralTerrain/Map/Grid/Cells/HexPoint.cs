using System;
using UnityEngine;

namespace SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells
{
    public class HexPoint
    {
        private static readonly float InradiusConstant = (float)(Math.Sqrt(3) / 2);

        public Point<int> GridPoint { get; }
        public Point<float> Center { get; }       

        public HexPoint(Point<int> gridPos, float gridRadius) : this(gridPos.X, gridPos.Y, gridRadius) { }
        
        public HexPoint(int gridX, int gridY, float gridRadius)
        {
            /*
             * To think about how a hex point center is gotten from a square grid
             * reference the guide below. Effectively, the odd x coordiante cells
             * represent the hex slightly above them
             *
             *          (-1. 1)        (1, 1)
             *  (-2, 1)         (0, 1) 
             *          (-1, 0)        (1, 0)
             *  (-2, 0)         (0, 0)
             *          (-1, -1)       (1, -1)
             */
            GridPoint = new Point<int>(gridX, gridY);
            float r = RadiusToInradius(gridRadius);
            float centerY = gridY * 2 * r;
            if (gridX % 2 != 0)
            {
                centerY += r;
            }
            float centerX = gridX * 1.5f * gridRadius;
            Center = new Point<float>(centerX, centerY);
        }

        /// <summary>
        /// Convert the grid point center to a vector 3
        /// </summary>
        /// <param name="y">The y value for the vector 3</param>
        /// <returns>The grid point center as a vector 3</returns>
        public Vector3 CenterAsVector3(float y)
        {
            return new Vector3(Center.X, y, Center.Y);
        }

        private static float RadiusToInradius(float r)
        {
            return InradiusConstant * r;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", GridPoint, Center);
        }
    }
}