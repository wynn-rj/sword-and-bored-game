using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilityButtonHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int num;
    [HideInInspector]
    public int highlightNum;
    [HideInInspector]
    public GameObject descriptionPanel;
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
        Debug.Log("Mouse is over GameObject.");
        descriptionPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse left button");
        descriptionPanel.SetActive(false);
    }
}
