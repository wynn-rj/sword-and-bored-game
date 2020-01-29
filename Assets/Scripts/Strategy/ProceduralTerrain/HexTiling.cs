using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTiling
{
    private bool[,] tiling;

    private int height;
    private int width;

    private int posCount;
    private int negCount;

    private float tileRatio;

    public HexTiling(int width, int height)
    {
        this.height = height;
        this.width = width;

        tileRatio = 0;

        tiling = new bool[width, height];
        GenerateTiling();
    }

    private void GenerateTiling()
    {

        int p = 0;

        while (p < 17)
        {
            float a = height - .9f * height;
            float b = width - .9f * width;

            float xCenter = Random.Range(0, width);
            float yCenter = Random.Range(0, height);

            Debug.Log(xCenter + ", " + yCenter);

            for (int i = 0; i < tiling.GetLength(0); i++)
            {
                for (int j = 0; j < tiling.GetLength(1); j++)
                {

                    //Pick a random center point

                    float partOne = Mathf.Pow(i - xCenter, 2) / Mathf.Pow(a, 2);
                    float partTwo = Mathf.Pow(j - yCenter, 2) / Mathf.Pow(b, 2);

                    if (partOne + partTwo < 1)
                    {
                        if (tiling[i, j] != true)
                        {
                            posCount += 1;

                            if (tiling[i, j] != false)
                            {
                                negCount -= 1;
                            }
                        }

                        tiling[i, j] = true;
                    }
                    else
                    {
                        if (tiling[i, j] != false)
                        {
                            negCount += 1;

                            if (tiling[i, j] != true)
                            {
                                posCount -= 1;
                            }
                        }

                        if (tiling[i, j] != true)
                        {
                            tiling[i, j] = false;
                        }
                    }
                }
            }
      
            tileRatio = (float)posCount / (width * height);
            Debug.Log(tileRatio);
            p += 1;
        }
    }

    private void TrimBorder()
    { 
    
    }

    public bool[,] GetTiling()
    {
        return tiling;
    }
}
