using System;
using UnityEngine;
using UnityEngine.UI;

public class ToggleUnitToSquad : MonoBehaviour
{
    private Action<GameObject> addAction;
    private Action<GameObject> removeAction;

    private bool added;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(HandleButtonClick);
        added = false;
    }

    public void Initialize(Action<GameObject> addAction, Action<GameObject> removeAction)
    {
        this.addAction = addAction;
        this.removeAction = removeAction;
    }

    private void HandleButtonClick()
    {
        if (!added)
        {
            addAction(gameObject);
            added = true;
            GetComponent<Button>().image.color = Color.red;
        }
        else
        {
            removeAction(gameObject);
            added = false;
            GetComponent<Button>().image.color = Color.white;
        }
    }
}
