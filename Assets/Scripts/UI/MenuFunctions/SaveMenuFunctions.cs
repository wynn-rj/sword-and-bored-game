using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SwordAndBored.GameData.Database;

namespace SwordAndBored.UI.MenuFunctions
{
    public class SaveMenuFunctions : MonoBehaviour
    {
        public TMP_Dropdown previousSaves;
        public TMP_InputField fileInput;
        public Button saveButton, overwriteButton, cancelButton;
        public Canvas returnCanvas;

        private List<string> oldFileNames;
        private List<string> emptyOption;
        // Start is called before the first frame update
        void Awake()
        {
            emptyOption = new List<string>
            {
                "----------Select A File----------"
            };
            previousSaves.ClearOptions();
            previousSaves.AddOptions(emptyOption);
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
                overwriteButton.interactable = true;
            }
            else
            {
                overwriteButton.interactable = false;
            }
        }

        public void OnTextInputChanged(string text)
        {
            if (text.Length > 0)
            {
                saveButton.interactable = true;
            }
            else
            {
                saveButton.interactable = false;
            }
        }

        public void SaveButtonPressed()
        {
            if (oldFileNames.Contains(fileInput.text))
            {
                // Open panel to confirm overwrite
                Debug.Log("File already exists with this name");
            }
            DatabaseManager.SaveData(fileInput.text);
            LeaveCanvas();
        }

        public void OverwriteButtonPressed()
        {
            DatabaseManager.SaveData(fileInput.text);
            LeaveCanvas();
        }

        public void CancelButtonPressed()
        {
            LeaveCanvas();
        }

        public void LeaveCanvas()
        {
            previousSaves.ClearOptions();
            previousSaves.AddOptions(emptyOption);
            oldFileNames = DatabaseManager.GetPreviousSaveNames();
            if (oldFileNames.Count > 0)
            {
                previousSaves.AddOptions(oldFileNames);
            }
            else
            {
                previousSaves.options[0].text = "No Previous Save Files";
            }
            returnCanvas.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}