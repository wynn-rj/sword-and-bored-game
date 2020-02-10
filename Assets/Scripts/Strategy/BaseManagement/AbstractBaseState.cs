using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractBaseState : IBaseManagementState
{
    protected BaseManager BaseManager { get; set; }

    public AbstractBaseState(BaseManager bm)
    {
        this.BaseManager = bm;
    }

    public virtual void Update() { }

    public virtual void SelectBuildingTier() { }

    public virtual void SelectBuilding() { }

    public virtual void PlaceBuilding()
    { 
    
    }
}
