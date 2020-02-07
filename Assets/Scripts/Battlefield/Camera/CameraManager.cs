using Cinemachine;
using SwordAndBored.Battlefield.TurnMechanism;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Battlefield.CameraUtilities
{

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
            if (turnManager.activePlayer.currentCamera.Priority < priority)
            {
                turnManager.activePlayer.currentCamera.Priority = priority + 1;
                priority++;
            }

        }
    }
}
