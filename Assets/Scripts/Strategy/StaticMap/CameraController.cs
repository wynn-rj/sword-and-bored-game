using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed;
    public int boundary;

    private int screenHeight;
    private int screenWidth;

    float ysensitivity = 15f;
    float zsensitivity = 10f;


    // Start is called before the first frame update
    void Start()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 zoom = new Vector3(transform.position.x, transform.position.y - (Input.GetAxis("Mouse ScrollWheel") * ysensitivity), transform.position.z + (Input.GetAxis("Mouse ScrollWheel") * zsensitivity));
        transform.position = zoom;

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
