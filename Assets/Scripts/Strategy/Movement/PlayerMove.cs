using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SwordAndBored.Strategy.ProceduralTerrain;
using SwordAndBored.Strategy.TimeSystem.TimeManager;
using SwordAndBored.Strategy.TimeSystem.Subscribers;


namespace SwordAndBored.Strategy.Movement
{
    public class PlayerMove : MonoBehaviour//, IPostTimeStepSubscriber
    {
        public TileManager tileManager;
        public bool usedMoveThisTurn = false;

        public Vector3 targetPosition;
        Vector3 lookAtTarget;
        Quaternion playerRot;
        float rotSpeed = 5;
        float speed = 10;
        bool moving = false;

        void Update()
        {
            if (moving)
            {
                Move();
            }
        }

        /// <summary>
        /// Tells the player where they are going to be moving based on mouse click
        /// </summary>
        public void SetTargetPosition()
        {
                this.transform.LookAt(targetPosition);
                lookAtTarget = new Vector3(targetPosition.x - transform.position.x, transform.position.y, targetPosition.z - transform.position.z);
                playerRot = Quaternion.LookRotation(lookAtTarget);
                moving = true;
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
            }
        }
    }

}