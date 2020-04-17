using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Strategy.Transitions;


[DefaultExecutionOrder(-50)]
public class BiomeManager : MonoBehaviour
{
    public GameObject mapTown;
    public GameObject mapPlain;
    public GameObject mapForest;
    public GameObject mapDesert;

    private void Awake()
    {

        string biomeString = SceneSharing.biome;


        switch (biomeString)
        {
            case "DesertTerrainComponent":
                mapDesert.SetActive(true);
                break;
            case "ForestTerrainComponent":
                mapForest.SetActive(true);
                break;
            case "GrasslandTerrainComponent":
                mapPlain.SetActive(true);
                break;
            case "Town":
                mapTown.SetActive(true);
                break;
            default:
                mapTown.SetActive(true);
                break;
        }
    }
}
