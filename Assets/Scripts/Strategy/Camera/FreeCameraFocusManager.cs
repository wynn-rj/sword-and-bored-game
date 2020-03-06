using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCameraFocusManager : MonoBehaviour
{
    private float rotateSpeed = 60;
    private float speed = 30;
    public CinemachineVirtualCamera freeCam;
    public Camera mainCam;
    private CinemachineBrain BigBrain;

    private void Start()
    {
        BigBrain = mainCam.GetComponent<CinemachineBrain>();
    }

    void Update()
    {
        
        move();
        rotate();
    }

    private void move()
    {
        Vector3 move = new Vector3();
        if (Input.GetKey(KeyCode.D))
        {
            move.x += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            move.x -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            move.z += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move.z -= speed * Time.deltaTime;
        }
        transform.Translate(move);
    }

    private void rotate()
    {
        if (Input.GetKey(KeyCode.E))
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(-Vector3.up * rotateSpeed * Time.deltaTime);
    }

}
