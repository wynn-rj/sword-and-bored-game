namespace SwordAndBored.Strategy.ProceduralTerrain.Map.Terrain
{
    interface ITerrainComponent : Grid.ICellComponent
    {
        int Height { get; set; }

        float WaterLevel { get; set; }
    }
}
