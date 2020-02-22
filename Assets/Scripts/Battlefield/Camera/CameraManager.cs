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
        private float freeCamSpeed = 20f;
        private float freeCamRotationSpeed = 80f;

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
            float translationX = Input.GetAxis("Horizontal") * freeCamSpeed;
           
            translationZ *= Time.deltaTime;
            translationX *= Time.deltaTime;

            
            freeCam.transform.position += new Vector3(0,0,translationZ);
            freeCam.transform.position += new Vector3(translationX, 0, translationZ);

        }

        private void rotateCamera()
        {
            if (Input.GetKeyDown("e"))
            {
                
            } else if (Input.GetKeyDown("q"))
            {

            }
        }

        private void SetPriority()
        {
            if (getActivePlayerCam().Priority < priority)
            {
                panToActivePlayer();
                freeCam.transform.position = getActivePlayerCam().transform.position;
                cameraDetached = false;
                priority++;
            }

        }

        private void panToActivePlayer()
        {
           getActivePlayerCam().Priority = priority + 1;
        }

        private CinemachineVirtualCamera getActivePlayerCam()
        {
            return turnManager.activePlayer.GetCam();
        }
    }
}
