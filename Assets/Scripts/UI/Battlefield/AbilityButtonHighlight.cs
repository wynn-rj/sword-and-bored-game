using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilityButtonHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [HideInInspector]
    public bool isSelected;
    // Start is called before the first frame update
    void Start()
    {
        isSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isSelected)
        {
            GetComponent<Image>().color = Color.green;
        } else
        {
            GetComponent<Image>().color = Color.white;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse left button");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Mouse clicked");
    }
}
