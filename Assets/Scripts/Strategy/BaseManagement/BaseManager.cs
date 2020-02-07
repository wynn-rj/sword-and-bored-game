using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    List<IBuilding> productionBuildings;

    List<IBuilding> resourceBuildings;

    private void Awake()
    {
        LoadBuildings();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void LoadBuildings()
    {
        
    }
}
