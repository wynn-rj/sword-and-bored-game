using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed;
    public int boundary;

    private int screenHeight;
    private int screenWidth;

    float minFOV = 15f;
    float maxFOV = 90f;
    float sensitivity = 10f;


    // Start is called before the first frame update
    void Start()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;
    }

    // Update is called once per frame
    void Update()
    {


        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFOV, maxFOV);
        Camera.main.fieldOfView = fov;


        Vector3 move = transform.position;
        if (Input.mousePosition.x > screenWidth - boundary)
        {
            
            move.x += speed * Time.deltaTime; // move on +X axis
        }
        if (Input.mousePosition.x < 0 + boundary)
        {
            move.x -= speed * Time.deltaTime; // move on -X axis
        }
        if (Input.mousePosition.y > screenHeight - boundary)
        {
            move.z += speed * Time.deltaTime; // move on +Z axis
        }
        if (Input.mousePosition.y < 0 + boundary)
        {
            move.z -= speed * Time.deltaTime; // move on -Z axis
        }
        transform.position = move;
    }
}
