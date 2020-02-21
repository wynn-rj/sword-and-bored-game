using UnityEngine;

public static class Constants
{
    public static readonly int hexRadius = 2;
    public static readonly int mapWidth = 100;
    public static readonly int mapHeight = 100;
    public static readonly int mountainHeightThreshold = 14;
    public static readonly int riverHeightThreshold = 7;
    public static readonly float riverWaterLevelThreshold = .8f;
    public static readonly float snowMountainWaterLvlThreshold = .9f;
    public static readonly float plainsWaterLevelThreshold = .8f;
    public static readonly float forestWaterLevelThreshold = .9f;
    public static readonly int snowMountainHeightThreshold = 20;
    public static readonly int xMargin = Mathf.FloorToInt(mapWidth * .05f);
    public static readonly int yMargin = Mathf.FloorToInt(mapHeight * .05f);
}
