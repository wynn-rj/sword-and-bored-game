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

    internal IBuilding CreateBarracks()
    {
        return new Barracks();
    }

    internal IBuilding CreateGranary()
    {
        return null;
    }
}
