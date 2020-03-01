using UnityEngine;

public static class Constants
{
    public static readonly int hexRadius = 4;
    public static readonly int mapWidth = 40;
    public static readonly int mapHeight = 40;
    public static readonly int mountainHeightThreshold = 22;
    public static readonly int riverHeightThreshold = 8;
    public static readonly float riverWaterLevelThreshold = .92f;
    public static readonly float snowMountainWaterLvlThreshold = .9f;
    public static readonly float plainsWaterLevelThreshold = .85f;
    public static readonly float forestWaterLevelThreshold = 1.0f;
    public static readonly int snowMountainHeightThreshold = 23;
    public static readonly int xMargin = Mathf.FloorToInt(mapWidth * .05f);
    public static readonly int yMargin = Mathf.FloorToInt(mapHeight * .05f);
    public static readonly string terrainObjectName = "TerrainTile";
    public static readonly string creepObjectName = "CreepTile";
}
