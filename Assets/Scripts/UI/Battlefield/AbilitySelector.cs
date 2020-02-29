using UnityEngine;
using UnityEngine.EventSystems;
using SwordAndBored.Battlefield.CreaturScripts;
using System.Collections.Generic;

public class AbilitySelector : MonoBehaviour
{
    public GameObject[] AbilityButtons;
    public GameObject descriptionPanel;
    [HideInInspector]
    public int currentlySelectedNum;
    [HideInInspector]
    public int highlightedNum;
    [HideInInspector]
    public List<Ability> AbilityList;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectedAbilityButton(1);
        } else if(Input.GetKeyDown(KeyCode.Alpha2)) {
            SelectedAbilityButton(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectedAbilityButton(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectedAbilityButton(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectedAbilityButton(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectedAbilityButton(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectedAbilityButton(7);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SelectedAbilityButton(8);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SelectedAbilityButton(9);
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

    public void ResetAbilitySelection(int num)
    {
        currentlySelectedNum = num;
        foreach (GameObject button in AbilityButtons)
        {
            button.GetComponent<AbilityButtonHighlight>().highlightNum = highlightedNum;
        }
    }

}
