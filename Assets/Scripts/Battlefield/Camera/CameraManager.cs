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
            if (cameraDetached == true) rotateCamera();

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
            float translationZ = Input.GetAxis("Vertical") * freeCamSpeed;
            float translationX = Input.GetAxis("Horizontal") * freeCamRotationSpeed;
            

           
            translationZ *= Time.deltaTime;
            translationX *= Time.deltaTime;

            
            freeCam.transform.Translate(0, 0, translationZ);
            freeCam.transform.Translate(translationX, 0, 0);

           
            //transform.Rotate(0, rotation, 0);
        }

        private void rotateCamera()
        {

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
