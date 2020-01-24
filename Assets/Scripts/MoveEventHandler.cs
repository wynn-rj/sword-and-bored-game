using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEventHandler : MonoBehaviour
{
    public delegate void MoveEventAction(Vector3 pos);
    public event MoveEventAction MoveCallbackEvent = delegate { };

    [SerializeField]
    private GameObject tileIndictor;

    [SerializeField]
    private LayerMask lm;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, lm))
        {
            GameObject tile = hit.collider.gameObject;
            Vector3 spot = tile.GetComponent<Tile>().GetPos();
            tileIndictor.transform.position = spot;
            if (Input.GetButtonDown("Fire1"))
            {
                MoveCallbackEvent(spot);
                //activePlayer.MoveTo(spot);
            }
        }
    }
}
