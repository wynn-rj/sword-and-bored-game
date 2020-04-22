using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSwitching : MonoBehaviour
{
    public Renderer top;
    public Renderer bot;
    public Transform head;

    public void SetColor(Material a, Material b, GameObject hat)
    {
        top.material = a;
        bot.material = b;
        if (hat)
        {
            GameObject hats = Instantiate(hat, head.position, Quaternion.identity);
            hats.transform.parent = head;
        }
    }
}
