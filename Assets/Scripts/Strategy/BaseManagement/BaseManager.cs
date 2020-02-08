using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    [SerializeField] GameObject canvasObject;
    [SerializeField] GameObject panel;

    Canvas canvas;

    List<IBuilding> productionBuildings;

    List<IBuilding> resourceBuildings;

    Vector3 placement;

    private void Awake()
    {
        canvas = canvasObject.GetComponent<Canvas>();


        AddBuildUIButtons();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleBuildingsList();
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 50))
        {
            Debug.Log("You selected the " + hit.point);
            placement = hit.point;
        }
    }

    private void AddBuildUIButtons()
    {
        //GameObject
    }

    private void ToggleBuildingsList()
    {
        canvasObject.SetActive(!canvasObject.activeSelf);
    }

    public void SelectBuildingPlacement(int index)
    {
       AbstractBuilding placedBuilding = buildingDict[index](placement);
       Instantiate(Resources.Load("Prefabs/" + placedBuilding.ModelName), placement, Quaternion.identity);
    }


    private void LoadBuildings()
    {

    }

    IDictionary<int, Func<Vector3, AbstractBuilding>> buildingDict = new Dictionary<int, Func<Vector3, AbstractBuilding>>()
    {
        {0, BuildingFactory.Instance.CreateBarracks},
        {1, BuildingFactory.Instance.CreateGranary }
    };
}
