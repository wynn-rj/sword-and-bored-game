using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    [SerializeField] GameObject canvasObject;
    [SerializeField] GameObject panel;

    List<IBuilding> productionBuildings;

    List<IBuilding> resourceBuildings;

    Vector3 placement;

    private Canvas canvas;

    public Canvas Canvas
    {
        get { return canvas; }
        set { canvas = value; }
    }

    private IBaseManagementState baseManagementState;

    public IBaseManagementState BaseManagementState
    {
        get { return baseManagementState; }
        set { baseManagementState = value; }
    }

    private int buildingIndex;

    public int BuildingIndex
    {
        get { return buildingIndex; }
        set { buildingIndex = value; }
    }

    [SerializeField]private int numberOfBuildings;

    private void Awake()
    {
        canvas = canvasObject.GetComponent<Canvas>();

        buildingIndex = 0;
        numberOfBuildings = 2;

        baseManagementState = new IdleBaseState(this);


        AddBuildUIButtons();
    }

    void Start()
    {

    }

    void Update()
    {
        baseManagementState.Update();
        Debug.Log(baseManagementState);
    }

    private void AddBuildUIButtons()
    {
        //GameObject
    }

    public void SelectBuilding(int index)
    {
        buildingIndex = index;
        baseManagementState = new PlaceBuildState(this);
    }

    public IBuilding GetBuilding(Vector3 position)
    {
        return buildingDict[buildingIndex](position);
    }

    IDictionary<int, Func<Vector3, IBuilding>> buildingDict = new Dictionary<int, Func<Vector3, IBuilding>>()
    {
        {0, BuildingFactory.Instance.CreateBarracks },
        {1, BuildingFactory.Instance.CreateGranary }
    };
}
