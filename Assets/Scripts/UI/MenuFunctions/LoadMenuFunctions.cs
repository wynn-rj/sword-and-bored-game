using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SwordAndBored.GameData.Database;

namespace SwordAndBored.UI.MenuFunctions
{
    public class LoadMenuFunctions : MonoBehaviour
    {
        public TMP_Dropdown previousSaves;
        public Button loadButton, cancelButton;
        public Canvas returnCanvas;

        private List<string> oldFileNames;
        // Start is called before the first frame update
        void Start()
        {
            oldFileNames = DatabaseManager.GetPreviousSaveNames();
            if (oldFileNames.Count > 0)
            {
                previousSaves.AddOptions(oldFileNames);
            }
            else
            {
                previousSaves.options[0].text = "No Previous Save Files";
            }
        }

        public void OnDropdownChanged(int newValue)
        {
            if (newValue > 0)
            {
                loadButton.interactable = true;
            }
            else
            {
                loadButton.interactable = false;
            }
        }

        public void LoadButtonPressed()
        {
            DatabaseManager.LoadData(previousSaves.options[previousSaves.value].text);
            returnCanvas.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        public void CancelButtonPressed()
        {
            returnCanvas.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}