using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BaseManager : MonoBehaviour
{
    //List<IBuilding> productionBuildings;
    //List<IBuilding> resourceBuildings;

    //The first element is the tiers option canvas
    [SerializeField] List<GameObject> buildingTierCanvases;

    [SerializeField] List<GameObject> tierButtonsList;

    public Button guess;

    [SerializeField] private int numberOfBuildings;

    private int activeTier;
    private int overallTier;
    private int buildingIndex;
    private Canvas activeCanvas;
    private IBaseManagementState baseManagementState;

    public Canvas ActiveCanvas
    {
        get { return activeCanvas; }
        set { activeCanvas = value; }
    }

    public IBaseManagementState BaseManagementState
    {
        get { return baseManagementState; }
        set { baseManagementState = value; }
    }

    public int BuildingIndex
    {
        get { return buildingIndex; }
        set { buildingIndex = value; }
    }

    public int ActiveTier
    {
        get { return activeTier; }
        set { activeTier = value; }
    }

    public int OverallTier
    {
        get { return overallTier; }
        set { overallTier = value; }
    }

    private void Awake()
    {
        activeCanvas = buildingTierCanvases[0].GetComponent<Canvas>();

        activeTier = overallTier = 1;
        buildingIndex = 0;
        numberOfBuildings = 2;

        baseManagementState = new IdleBaseState(this);
    }

    void Start() { }

    void Update()
    {
        baseManagementState.Update();
        Debug.Log(baseManagementState);
    }

    public void ToggleActiveCanvas()
    {
        activeCanvas.gameObject.SetActive(!activeCanvas.gameObject.activeSelf);
    }

    public void SetActiveCanvas(int index)
    {
        activeCanvas = buildingTierCanvases[index].GetComponent<Canvas>();
    }

    /// <summary>
    /// Activates proper tier list canvas of buildings and ensures all others are deactivated
    /// </summary>
    /// <param name="index"></param>
    public void SetAndToggleActiveCanvas(int index)
    {
        if (index <= overallTier)
        {
            ToggleActiveCanvas();
            SetActiveCanvas(index);
            ToggleActiveCanvas();
        }
    }

    public void SelectBuildingTier(int index)
    {
        activeTier = index;
        baseManagementState.SelectBuildingTier();
    }

    public void SelectBuilding(int index)
    {
        buildingIndex = index;
        baseManagementState.SelectBuilding();
    }

    public IBuilding GetBuilding(int tier)
    {
        return buildingDict[tier][buildingIndex]();
    }

    public void UnlockTier(int tier)
    {
        overallTier = tier;
        tierButtonsList[tier - 1].SetActive(true);
    }

    public void SetAllCanvasInactive()
    {
        foreach (GameObject canvasObject in buildingTierCanvases)
        {
            canvasObject.SetActive(false);
        }
    }

    /*IDictionary<int, Func<IBuilding>> tierIDict = new Dictionary<int, Func<IBuilding>>()
    {
        {0, BuildingFactory.Instance.CreateBarracks },
        {1, BuildingFactory.Instance.CreateGranary }
    };*/
                
    IDictionary<int, Dictionary<int, Func<IBuilding>>> buildingDict = new Dictionary<int, Dictionary<int, Func<IBuilding>>>()
    {
        //Tier I buildingd
        {1, new Dictionary<int, Func<IBuilding>>()
            {
                {0, BuildingFactory.Instance.CreateBarracks },
                {1, BuildingFactory.Instance.CreateGranary }
            }
        },

        //Tier II buildingd
        {2, new Dictionary<int, Func<IBuilding>>()
            {
                {0, BuildingFactory.Instance.CreateBarracks },
                {1, BuildingFactory.Instance.CreateGranary }
            }
        },

        //Tier III buildingd
        {3, new Dictionary<int, Func<IBuilding>>()
            {
                {0, BuildingFactory.Instance.CreateBarracks },
                {1, BuildingFactory.Instance.CreateGranary }
            }
        }
    };  
}
