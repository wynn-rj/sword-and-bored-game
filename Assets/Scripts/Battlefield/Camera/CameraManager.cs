using Cinemachine;
using SwordAndBored.BattleMechanism;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public TurnManager turnManager;
    public GameObject[] cameras = new GameObject[5];
    private int priority = 1;

    void Start()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].GetComponent<CinemachineVirtualCamera>().Priority = 0;
        }
    }


    void Update()
    {
        SetPriority();
    }

    private void SetPriority()
    {
        if (turnManager.activePlayer.gameObject.transform.GetChild(3).GetComponent<CinemachineVirtualCamera>().Priority < priority)
        {
            turnManager.activePlayer.gameObject.transform.GetChild(3).GetComponent<CinemachineVirtualCamera>().Priority = priority + 1;
            priority++;
        }

    }
}
