using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFactory
{
    private static BuildingFactory instance;

    private BuildingFactory() { }

    public static BuildingFactory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BuildingFactory();
            }

            return instance;
        }
    }

    internal IBuilding CreateBarracks(Vector3 position)
    {
        return new Barracks(position);
    }

    internal IBuilding CreateGranary(Vector3 position)
    {
        return null;
    }
}
