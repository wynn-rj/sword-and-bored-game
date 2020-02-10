using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace SwordAndBored.StrategyView.Movement
{
    public class PlayerMove : MonoBehaviour
    {
        Vector3 targetPosition;
        Vector3 lookAtTarget;
        Quaternion playerRot;
        float rotSpeed = 5;
        float speed = 10;
        bool moving = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                SetTargetPosition();
            }
            if (moving)
            {
                Move();
            }
        }

        void SetTargetPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10000))
            {
                Vector3 center = new Vector3(hit.collider.gameObject.transform.position.x, this.transform.position.y, hit.collider.gameObject.transform.position.z);
                targetPosition = center;
                this.transform.LookAt(targetPosition);
                lookAtTarget = new Vector3(targetPosition.x - transform.position.x, transform.position.y, targetPosition.z - transform.position.z);
                playerRot = Quaternion.LookRotation(lookAtTarget);
                moving = true;
            }
        }

        void Move()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, rotSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                moving = false;
            }
        }

    }

}