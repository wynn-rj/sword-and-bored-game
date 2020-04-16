using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(-50)]
public class BiomeManager : MonoBehaviour
{
    public GameObject mapTown;
    public GameObject mapPlain;
    public GameObject mapForest;
    public GameObject mapCreep;
    public GameObject mapDesert;
    public int mapType;

    private void Awake()
    {

        

        if (mapType == 0)
        {
            mapTown.SetActive(true);
        } else if (mapType == 1)
        {
            mapPlain.SetActive(true);
        }
        else if (mapType == 2)
        {
            mapForest.SetActive(true);
        }
        else if (mapType == 3)
        {
            mapCreep.SetActive(true);
        }
        else if (mapType == 4)
        {
            mapDesert.SetActive(true);
        }
    }
}
