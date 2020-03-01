namespace SwordAndBored.Strategy.ProceduralTerrain.Map.Terrain
{
    abstract class AbstractTerrainComponent : Grid.AbstractCellComponent, ITerrainComponent
    {
        public int Height { get; set; }
        public float WaterLevel { get; set; }
    }
}
