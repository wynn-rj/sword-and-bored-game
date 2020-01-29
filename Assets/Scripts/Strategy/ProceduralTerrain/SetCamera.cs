using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCamera : MonoBehaviour
{

    public int x;
    public int y;
    public int z;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(x, y, z);
        //gameObject.transform.rotation = new Quaternion(-60, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
