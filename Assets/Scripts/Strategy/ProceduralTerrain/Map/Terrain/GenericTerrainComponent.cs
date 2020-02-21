using UnityEngine;

namespace SwordAndBored.Strategy.ProceduralTerrain.Map.Terrain
{
    class GenericTerrainComponent : Grid.AbstractCellComponent, ITerrainComponent
    {
        public int Height { get; set; }
        public float WaterLevel { get; set; }
    }
}
