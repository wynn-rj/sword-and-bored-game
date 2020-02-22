using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SwordAndBored.Strategy.ProceduralTerrain;


namespace SwordAndBored.StrategyView.Movement
{
    public class PlayerMove : MonoBehaviour
    {
        public TileManager tileManager;
        public Material selectedMaterial;

        Vector3 targetPosition;
        Vector3 lookAtTarget;
        Quaternion playerRot;
        float rotSpeed = 5;
        float speed = 10;
        bool moving = false;
        bool selected;
        
        public Material defaultMaterial;

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Clicked();
            }
            /*if(selected)
            {
                if(Input.GetMouseButton(0))
                {
                    SetTargetPosition();
                }
            }*/
            if (moving)
            {
                Move();
            }
        }

        void Clicked()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject == this.gameObject)
                {
                    selected = true;
                    this.GetComponent<MeshRenderer>().material = selectedMaterial;
                }
                else
                {
                    if(selected)
                    {
                        SetTargetPosition();
                    }
                    this.GetComponent<MeshRenderer>().material = defaultMaterial;
                    selected = false;
                }
            }
            /*if(tileManager.GetComponent<TileSelect>().lastClicked == this.gameObject)
            {
                selected = true;
                this.GetComponent<MeshRenderer>().material = selectedMaterial;
            }*/
        }

        /// <summary>
        /// Tells the player where they are going to be moving based on mouse click
        /// </summary>
        void SetTargetPosition()
        {
            //Finds the center of the hexagon that has been clicked
            if(tileManager.GetComponent<TileSelect>().center != new Vector3(0f,0f,0f))
            {
                targetPosition = tileManager.GetComponent<TileSelect>().center;

                this.transform.LookAt(targetPosition);
                lookAtTarget = new Vector3(targetPosition.x - transform.position.x, transform.position.y, targetPosition.z - transform.position.z);
                playerRot = Quaternion.LookRotation(lookAtTarget);
                moving = true;
                this.GetComponent<Rigidbody>().useGravity = false;
            }
        }

        /// <summary>
        /// Moves player to position, stops once the player has reached the position
        /// </summary>
        void Move()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, rotSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                moving = false;
                this.GetComponent<Rigidbody>().useGravity = true;
            }
        }

    }

}