using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGrid
{
    [SerializeField] private int width;
    [SerializeField] private int height;



    private static BaseGrid instance;

    private BaseGrid() { }

    public static BaseGrid Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BaseGrid();
            }

            return instance;
        }
    }

    public Vector3 ReturnGridPoint(Vector3 position)
    {
        return Vector3.zero;
    }
}
