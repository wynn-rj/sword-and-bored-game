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
        public bool cameraDetached = false;
        private int priority = 1;
        public float freeCamSpeed = 10f;
        public float freeCamRotationSpeed = 10f;

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

            if(cameraDetached == true) moveCamera();

            if (cameraDetached == false && Input.GetKeyDown("space"))
            {
                cameraDetached = true;
                freeCam.Priority = priority + 1;
            } else if (cameraDetached == true && Input.GetKeyDown("space"))
            {
                cameraDetached = false;
                freeCam.Priority = 0;
                panToActivePlayer();
                
            }

           
        }

        private void moveCamera()
        {
            float translation = Input.GetAxis("Vertical") * freeCamSpeed;
            float rotation = Input.GetAxis("Horizontal") * freeCamRotationSpeed;
            

           
            translation *= Time.deltaTime;
            rotation *= Time.deltaTime;

            
            freeCam.transform.Translate(0, 0, translation);
            freeCam.transform.Rotate(0, rotation, 0);

           
            //transform.Rotate(0, rotation, 0);
        }

        private void SetPriority()
        {
            if (turnManager.activePlayer.GetCam().Priority < priority)
            {
                panToActivePlayer();
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
