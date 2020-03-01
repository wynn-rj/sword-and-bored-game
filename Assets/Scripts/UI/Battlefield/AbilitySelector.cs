using UnityEngine;
using System.Collections.Generic;
using SwordAndBored.Battlefield.CreaturScripts;

public class AbilitySelector : MonoBehaviour
{
    public GameObject[] AbilityButtons;
    public GameObject descriptionPanel;
    [HideInInspector]
    public int currentlySelectedNum;
    [HideInInspector]
    public int highlightedNum;
    [HideInInspector]
    public List<Ability> abilityList;
    [HideInInspector]
    public bool setAbilityList;
    

    private KeyCode[] keyCodes = {
             KeyCode.Alpha1,
             KeyCode.Alpha2,
             KeyCode.Alpha3,
             KeyCode.Alpha4,
             KeyCode.Alpha5,
             KeyCode.Alpha6,
             KeyCode.Alpha7,
             KeyCode.Alpha8,
             KeyCode.Alpha9,
         };
    // Start is called before the first frame update
    void Start()
    {
        currentlySelectedNum = -1;
        highlightedNum = -1;

        foreach (GameObject button in AbilityButtons)
        {
            button.GetComponent<AbilityButtonHighlight>().descriptionPanel = descriptionPanel;
        }
        descriptionPanel.SetActive(false);
        setAbilityList = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (setAbilityList)
        {
            for (int i = 0; i < abilityList.Count; i++)
            {
                if (Input.GetKeyDown(keyCodes[i]))
                {
                    SelectedAbilityButton(i + 1);
                    DisplayDescriptiorPanel(i + 1);
                } else if(Input.GetKeyUp(keyCodes[i]))
                {
                    TurnOffDescriptorPanel(i + 1);
                }
            }
        }
    }

    public void SelectedAbilityButton(int num)
    {
        currentlySelectedNum = num;
        highlightedNum = num;
        foreach (GameObject button in AbilityButtons)
        {
            button.GetComponent<AbilityButtonHighlight>().highlightNum = highlightedNum;
        }
    }

    public void DisplayDescriptiorPanel(int num)
    {
        AbilityButtons[num - 1].GetComponent<AbilityButtonHighlight>().OnPointerEnter(null);
    }

    public void TurnOffDescriptorPanel(int num)
    {
        AbilityButtons[num - 1].GetComponent<AbilityButtonHighlight>().OnPointerExit(null);
    }

    public void ResetAbilitySelection(int num)
    {
        currentlySelectedNum = num;
        foreach (GameObject button in AbilityButtons)
        {
            button.GetComponent<AbilityButtonHighlight>().highlightNum = highlightedNum;
        }
    }
}
