using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowEntryModel : MonoBehaviour
{
    private Transform displayModel;
    private Action<Transform> clickAction;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(HandleButtonClick);
    }


    public void Initialize(Transform displayModel, Action<Transform> clickAction)
    {
        this.displayModel = displayModel;
        this.clickAction = clickAction;
    }

    private void HandleButtonClick()
    {
        clickAction(displayModel);
    }
}
