using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    void FixedUpdate()
    {
        transform.rotation = cam.transform.rotation;
    }
}
