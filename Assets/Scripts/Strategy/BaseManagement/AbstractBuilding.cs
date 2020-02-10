using SwordAndBored.GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBuilding : IBuilding
{
    public Canvas HUD { get; set; }

    public int Tier { get; set; }

    public float BuildTime { get; set; }

    public string ModelName { get; set; }

    public string ShadowModelName { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public AbstractBuilding()
    {
        ShadowModelName = "PlaceableBuilding";
    }

}
