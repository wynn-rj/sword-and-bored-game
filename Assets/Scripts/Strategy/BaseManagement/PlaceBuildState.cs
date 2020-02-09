using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBuildState : AbstractBaseState
{
    private Vector3 position;
    private RaycastHit hit;
    private Ray ray;
    

    public PlaceBuildState(BaseManager bm) : base(bm)
    {
        ToggleBuildingsList();
    }

    public override void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 50))
        {
            position = hit.point;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click Registered");
            IBuilding placedBuilding = BaseManager.GetBuilding(position);
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = position;
            //BaseManager.Instantiate(PrimitiveType.Cube, position, Quaternion.identity);
            BaseManager.BaseManagementState = new IdleBaseState(BaseManager);
        }

        base.Update();
    }
}
