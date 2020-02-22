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
        
        public Material defaultMaterial;

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                OnClick();
            }
            if (moving)
            {
                Move();
            }
        }

        void OnClick()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject == this.gameObject)
            {
                //if(hit.collider.gameObject == this.gameObject)
                //{
                    this.GetComponent<MeshRenderer>().material = selectedMaterial;
                //}
            }
        }

        /// <summary>
        /// Tells the player where they are going to be moving based on mouse click
        /// </summary>
        public void SetTargetPosition()
        {
            //New logic needed here, something to keep the target from being a null reference
            if(tileManager.GetComponent<TileSelect>().center != new Vector3(-50f,-50f,-50f))
            {
                targetPosition = tileManager.GetComponent<TileSelect>().center;
                this.GetComponent<Rigidbody>().isKinematic = false;

                this.transform.LookAt(targetPosition);
                lookAtTarget = new Vector3(targetPosition.x - transform.position.x, transform.position.y, targetPosition.z - transform.position.z);
                playerRot = Quaternion.LookRotation(lookAtTarget);
                moving = true;
            }
        }

        /// <summary>
        /// Moves player to position, stops once the player has reached the position
        /// </summary>
        void Move()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, rotSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            if(Physics.Raycast(transform.position, fwd, out RaycastHit hit, 0.5f))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
            }



            if (transform.position == targetPosition)
            {
                moving = false;
                this.GetComponent<Rigidbody>().isKinematic = true;
                this.GetComponent<MeshRenderer>().material = defaultMaterial;
            }
        }

    }

}