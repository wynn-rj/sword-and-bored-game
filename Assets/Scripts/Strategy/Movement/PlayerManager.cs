using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Strategy.TimeSystem.TimeManager;
using SwordAndBored.Strategy.TimeSystem.Subscribers;
using SwordAndBored.Strategy.Movement;
using SwordAndBored.Strategy.ProceduralTerrain;

public class PlayerManager : MonoBehaviour, IPreTimeStepSubscriber
{
    public List<GameObject> children;
    public Material defaultMaterial;
    public Material selectedMaterial;
    public TimeTrackingTimeManager turnManager;
    public TileManager tileManager;

    private GameObject selectedPlayer;
    private TileSelect tileSelect;

    void Start()
    {
        Transform[] tList = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform t in transform)
        {
            children.Add(t.gameObject);
        }
        turnManager.Subscribe(this);
        tileSelect = tileManager.GetComponent<TileSelect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            OnClick();
        }
    }

    void OnClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) && children.Contains(hit.collider.gameObject))
        {
            selectedPlayer = hit.collider.gameObject;
            selectedPlayer.GetComponent<MeshRenderer>().material = selectedMaterial;
            foreach (GameObject g in children)
            {
                if (g != hit.collider.gameObject)
                {
                    g.GetComponent<MeshRenderer>().material = defaultMaterial;
                }
            }
        }
    }

    public void SetPlayerAndTarget()
    {
        if (!(tileSelect.lastSelectedTile is null || selectedPlayer.GetComponent<PlayerMove>().usedMoveThisTurn))
        {
            selectedPlayer.GetComponent<PlayerMove>().targetPosition = tileManager.GetComponent<TileSelect>().tilePosition;
            selectedPlayer.GetComponent<Rigidbody>().isKinematic = false;
            selectedPlayer.GetComponent<PlayerMove>().usedMoveThisTurn = true;
            selectedPlayer.GetComponent<PlayerMove>().SetTargetPosition();
        }
        selectedPlayer.GetComponent<MeshRenderer>().material = defaultMaterial;
    }

    public void PreTimeStepUpdate()
    {
        foreach(GameObject g in children)
        {
            g.GetComponent<PlayerMove>().usedMoveThisTurn = false;
        }
    }
}
