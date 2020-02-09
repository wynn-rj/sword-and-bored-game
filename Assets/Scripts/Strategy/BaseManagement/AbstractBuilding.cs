using SwordAndBored.GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBuilding : IBuilding
{
    public float BuildTime { get; set; }

    public string ModelName { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public AbstractBuilding(Vector3 position) { }

}
