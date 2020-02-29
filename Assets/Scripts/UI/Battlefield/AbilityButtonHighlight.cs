using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilityButtonHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int num;
    [HideInInspector]
    public int highlightNum;
    // Start is called before the first frame update
    void Start()
    {
        highlightNum = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if(num == highlightNum)
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
}
