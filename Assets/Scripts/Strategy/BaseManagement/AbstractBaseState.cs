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

    public virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleBuildingsList();
            BaseManager.BaseManagementState = new SelectBuildState(BaseManager);
        }
    }

    protected virtual void ToggleBuildingsList()
    {
        BaseManager.Canvas.gameObject.SetActive(!BaseManager.Canvas.gameObject.activeSelf);
    }
}
