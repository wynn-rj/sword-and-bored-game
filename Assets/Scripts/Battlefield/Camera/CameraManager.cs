﻿using Cinemachine;
using SwordAndBored.Battlefield.TurnMechanism;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Battlefield.CameraUtilities
{

    public class CameraManager : MonoBehaviour
    {

        public TurnManager turnManager;
        public List<GameObject> cameras = new List<GameObject>();
        public CinemachineVirtualCamera freeCam;
        public bool cameraDetached = false;
        private int priority = 1;

        void Start()
        {
            for (int i = 0; i < cameras.Count; i++)
            {
                cameras[i].GetComponent<CinemachineVirtualCamera>().Priority = 0;
            }
        }


        void Update()
        {
            SetPriority();
            if (cameraDetached == false && Input.GetKeyDown("space"))
            {
                cameraDetached = true;
                freeCam.Priority = priority + 1;
            } else if (cameraDetached == true && Input.GetKeyDown("space"))
            {
                cameraDetached = false;
                
            }
            
           
        }

        private void SetPriority()
        {
            if (turnManager.activePlayer.GetCam().Priority < priority)
            {
                panToActivePlayer;
                cameraDetached = false;
                priority++;
            }

        }

        private void panToActivePlayer()
        {
            turnManager.activePlayer.GetCam().Priority = priority + 1;
        }
    }
}
