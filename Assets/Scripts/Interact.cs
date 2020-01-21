using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public GameObject indicator;
    public LayerMask lm;

    public UnitBase activePlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, lm))
        {
            Vector3 spot = hit.collider.GetComponent<Tile>().GetPos();
            Debug.Log(hit.collider.name);
            indicator.transform.position = spot;
            if (Input.GetButtonDown("Fire1"))
            {
                activePlayer.SetDestination(spot);
            }
        }
    }
}
