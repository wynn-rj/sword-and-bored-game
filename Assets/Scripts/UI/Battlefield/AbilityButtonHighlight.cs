using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SwordAndBored.Battlefield.CreaturScripts;

public class AbilityButtonHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int num;
    [HideInInspector]
    public int highlightNum;
    [HideInInspector]
    public GameObject descriptionPanel;
    [HideInInspector]
    public Ability ability;
    [HideInInspector]
    public bool isEnabled;
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
            GetComponent<Image>().color = Color.black;
        } else
        {
            GetComponent<Image>().color = Color.white;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isEnabled)
        {
            descriptionPanel.SetActive(true);
            descriptionPanel.GetComponent<AbilityDescriptionPanel>().DisplayDataInUI(ability);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isEnabled)
        {
            descriptionPanel.SetActive(false);
        }
    }
}
