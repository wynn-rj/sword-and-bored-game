using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGUIManager : MonoBehaviour
{
    [SerializeField] GameObject canvasObject;

    Canvas canvas;

    private void Awake()
    {
        canvas = canvasObject.GetComponent<Canvas>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleBuildingsList();
        }
    }

    private void ToggleBuildingsList()
    {
        canvasObject.SetActive(!canvasObject.activeSelf);
    }
}
