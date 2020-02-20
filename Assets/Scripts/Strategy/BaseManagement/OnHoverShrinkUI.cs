using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnHoverShrinkUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    RectTransform rt;

    private void Awake()
    {
        rt = gameObject.GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(.99f, .99f, 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    { 
        transform.localScale = new Vector3(1, 1, 1);
    }
}
