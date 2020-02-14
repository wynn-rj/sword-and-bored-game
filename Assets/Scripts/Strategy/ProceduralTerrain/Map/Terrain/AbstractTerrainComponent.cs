using UnityEngine;

namespace SwordAndBored.StrategyView.Map.Terrain
{
    class AbstractTerrainComponent : Grid.AbstractCellComponent, ITerrainComponent
    {
        private int height;

        public int Height
        {
            get
            {
                //This commented code should stay, currently taking out the perlin noise so that the map is flat for simple movement
                return Mathf.FloorToInt(Mathf.PerlinNoise(Parent.Position.GridPoint.X / 25f + 1000, Parent.Position.GridPoint.Y / 25f + 1000) * 30);
                //return 0;
            }

            set
            {
                height = value;
            }
        }

        private float waterLevel;

        public float WaterLevel
        {
            get
            {
                //Water Level with x^3
                return (Mathf.Pow(((Parent.Position.GridPoint.X + (Constants.mapWidth / 2f)) / Constants.mapWidth) - 0.5f, 3) + 0.5f) * (Mathf.Pow((((Parent.Position.GridPoint.Y + (Constants.mapHeight / 2f)) / Constants.mapHeight) + 0.5f), (1 / 3f)) + 0.75f) + Random.Range(0, 0.035f);//(Mathf.Pow(((Parent.Position.GridPoint.Y + (Constants.mapHeight / 2f)) / Constants.mapHeight) + 0.5f, 3) + 0.5f);
                //**Water Level with Perlin Noise**
                //Mathf.FloorToInt(Mathf.PerlinNoise(Parent.Position.GridPoint.X / 25f + 2000, Parent.Position.GridPoint.Y / 25f + 2000) * 30); ; 
            }

            set
            {
                waterLevel = value;
            }
        }

        public AbstractTerrainComponent()
        {
            this.height = 0;
            this.waterLevel = 0;
        }
    }
}
