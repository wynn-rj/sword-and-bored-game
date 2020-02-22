using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class OrthographicZoomStrategy : IZoomStrategy
{

    public OrthographicZoomStrategy(CinemachineVirtualCamera cam, float startingZoom)
    {
        cam.m_Lens.OrthographicSize = startingZoom;
    }

    public void ZoomIn(CinemachineVirtualCamera cam, float delta, float nearZoomLimit)
    {
        if (cam.m_Lens.OrthographicSize == nearZoomLimit) return;
        cam.m_Lens.FieldOfView = Mathf.Max(cam.m_Lens.FieldOfView + delta, nearZoomLimit);
    }

    public void ZoomOut(CinemachineVirtualCamera cam, float delta, float farZoomLimit)
    {
        if (cam.m_Lens.OrthographicSize == farZoomLimit) return;
        cam.m_Lens.FieldOfView = Mathf.Min(cam.m_Lens.FieldOfView - delta, farZoomLimit);
        
    }
}
