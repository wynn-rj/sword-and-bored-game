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
        public List<GameObject> cameras = new List<GameObject>();
        public CinemachineVirtualCamera freeCam;
        private bool cameraDetached = false;
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
            if (Input.GetKeyDown("space") && cameraDetached == false)
            {
                cameraDetached = true;
                freeCam.Priority = priority + 1;
                priority++;
            } else if (Input.GetKeyDown("space") && cameraDetached == true)
            {
                cameraDetached = false;
            }
            
           
        }

        private void SetPriority()
        {
            if (turnManager.activePlayer.GetCam().Priority < priority)
            {
                turnManager.activePlayer.GetCam().Priority = priority + 1;
                cameraDetached = false;
                priority++;
            }

        }
    }
}
