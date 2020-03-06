using Cinemachine;
using SwordAndBored.Strategy.Squads;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyCameraSystemManager : MonoBehaviour
{
    public CinemachineVirtualCamera freeCam;
    public SquadManager squadManager;
    public CinemachineVirtualCamera squadCam;

    void Start()
    {
        freeCam.Priority = 1;
    }

    void Update()
    {
        if (squadManager.SelectedSquad)
        {
            squadCam = squadManager.SelectedSquad.GetComponent<CinemachineVirtualCamera>();
            freeCam.Priority = 0;
            squadCam.Priority = 1;
        } else
        {
            squadCam.Priority = 0;
            freeCam.Priority = 1;
        }
    }
}
