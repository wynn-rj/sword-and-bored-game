using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public GameObject tileIndictor;
    public LayerMask lm;

    public CreatureBase activePlayer;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, lm))
        {
            Vector3 spot = hit.collider.GetComponent<Tile>().GetPos();
            tileIndictor.transform.position = spot;
            if (Input.GetButtonDown("Fire1"))
            {
                activePlayer.MoveTo(spot);
            }
        }
    }
}
