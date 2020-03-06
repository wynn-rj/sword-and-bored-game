using Cinemachine;
using SwordAndBored.Strategy.Squads;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyCameraSystemManager : MonoBehaviour
{
    public CinemachineVirtualCamera freeCam;
    public GameObject freeCamFocus;
    public SquadManager squadManager;
    private CinemachineVirtualCamera squadCam;

    void Start()
    {
        freeCam.Priority = 1;
    }

    void Update()
    {
        if (squadManager.SelectedSquad)
        {
            freeCam.m_Follow = squadManager.SelectedSquad.gameObject.transform;
            freeCam.m_LookAt = squadManager.SelectedSquad.gameObject.transform;
        } else
        {
            squadCam.Priority = 0;
            freeCam.Priority = 1;
        }
    }
}
