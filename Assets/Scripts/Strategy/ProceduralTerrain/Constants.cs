using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public static readonly int hexRadius = 1;
    public static readonly int mapWidth = 20;  //Changed from 200->20 for display purposes
    public static readonly int mapHeight = 20; //Changed from 200->20 for display purposes
    public static readonly int mountainHeightThreshold = 7;
    public static readonly int riverHeightThreshold = 0; //Changed from 15->0 so that river wouldn't spawn on map
    public static readonly float riverWaterLevelThreshold = .8f;
    public static readonly float snowMountainWaterLvlThreshold = .9f;
    public static readonly float plainsWaterLevelThreshold = .8f;
    public static readonly float forestWaterLevelThreshold = .9f;
    public static readonly int snowMountainHeightThreshold = 10;
    public static readonly int xMargin = Mathf.FloorToInt(mapWidth * .05f);
    public static readonly int yMargin = Mathf.FloorToInt(mapHeight * .05f);
}
