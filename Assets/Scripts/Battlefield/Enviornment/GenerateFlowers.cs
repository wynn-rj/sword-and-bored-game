using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFlowers : MonoBehaviour
{
    Renderer r;
    public float amount = .1f;
    public GameObject flower;

    // Start is called before the first frame update
    void Awake()
    {
        r = GetComponent<Renderer>();
        float x = r.bounds.size.x;
        float y = r.bounds.size.z;

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; i < y; j++)
            {
                float noise = Mathf.PerlinNoise(x, y);
                if (noise > (1f - amount))
                {
                    Instantiate(flower, new Vector3(x, .01f, y), Quaternion.identity, transform);
                }
            }
        }
    }
}
