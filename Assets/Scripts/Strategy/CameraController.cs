﻿using UnityEngine;

namespace SwordAndBored.Strategy
{
    public class CameraController : MonoBehaviour
    {
        public float speed;
        public int boundary;

        private int screenHeight;
        private int screenWidth;

        readonly float ysensitivity = 15f;
        readonly float zsensitivity = 10f;

        void Start()
        {
            screenHeight = Screen.height;
            screenWidth = Screen.width;
        }

        void Update()
        {
            Vector3 zoom = new Vector3(transform.position.x, transform.position.y - (Input.GetAxis("Mouse ScrollWheel") * ysensitivity), transform.position.z + (Input.GetAxis("Mouse ScrollWheel") * zsensitivity));
            transform.position = zoom;

            Vector3 move = transform.position;
            if (Input.mousePosition.x > screenWidth - boundary || Input.GetKey(KeyCode.RightArrow))
            {
                move.x += speed * Time.deltaTime;
            }
            if (Input.mousePosition.x < 0 + boundary || Input.GetKey(KeyCode.LeftArrow))
            {
                move.x -= speed * Time.deltaTime;
            }
            if (Input.mousePosition.y > screenHeight - boundary || Input.GetKey(KeyCode.UpArrow))
            {
                move.z += speed * Time.deltaTime;
            }
            if (Input.mousePosition.y < 0 + boundary || Input.GetKey(KeyCode.DownArrow))
            {
                move.z -= speed * Time.deltaTime;
            }
            transform.position = move;
        }
    }
}