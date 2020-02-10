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
    [SerializeField] List<UnityEngine.UI.Button> tierButtonsList;

    [SerializeField] private int numberOfBuildings;

    private int activeTier;
    private int overallTier;
    private int buildingIndex;
    private Canvas activeCanvas;
    private IBaseManagementState baseManagementState;

    public BaseGrid BaseGrid;

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
        BaseGrid = FindObjectOfType<BaseGrid>();

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
        tierButtonsList[tier - 1].interactable = true;
    }

    public void SetAllCanvasInactive()
    {
        foreach (GameObject canvasObject in buildingTierCanvases)
        {
            canvasObject.SetActive(false);
        }
    }

    public void ExitCanvas()
    {
        baseManagementState.Exit();
    }

    IDictionary<int, Dictionary<int, Func<IBuilding>>> buildingDict = new Dictionary<int, Dictionary<int, Func<IBuilding>>>()
    {
        //Tier I buildings
        {1, new Dictionary<int, Func<IBuilding>>()
            {
                {0, BuildingFactory.Instance.CreateBarracks },
                {1, BuildingFactory.Instance.CreateGranary }
            }
        },

        //Tier II buildings
        {2, new Dictionary<int, Func<IBuilding>>()
            {
                //Placeholders
                {0, BuildingFactory.Instance.CreateBarracks },
                {1, BuildingFactory.Instance.CreateGranary }
            }
        },

        //Tier III buildings
        {3, new Dictionary<int, Func<IBuilding>>()
            {
                //Placeholders
                {0, BuildingFactory.Instance.CreateBarracks },
                {1, BuildingFactory.Instance.CreateGranary }
            }
        }
    };  
}
