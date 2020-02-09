using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBuildState : AbstractBaseState
{
    public SelectBuildState(BaseManager bm) : base(bm)
    { 
    
    }

    public override void SelectBuilding()
    {
        IBuilding selectedBuilding = BaseManager.GetBuilding();
        BaseManager.BaseManagementState = new PlaceBuildState(BaseManager, selectedBuilding);

        base.SelectBuilding();
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleBuildingsList();
            BaseManager.BaseManagementState = new IdleBaseState(BaseManager);
        }
    }
}
