using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IZoomStrategy
{
    void ZoomIn(CinemachineVirtualCamera cam, float delta, float nearZoomLimit);
    void ZoomOut(CinemachineVirtualCamera cam, float delta, float farZoomLimit);
}
