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
        public Button saveButton, overwriteButton;
        // Start is called before the first frame update
        void Start()
        {
            List<string> oldFilenames = DatabaseManager.GetPreviousSaveNames();
            if(oldFilenames.Count > 0)
            {
                previousSaves.AddOptions(oldFilenames);
            } else
            {
                previousSaves.options[0].text = "No Previous Save Files";
            }
        }

        // Update is called once per frame
        void Update()
        {
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
            //Handle save text == option
        }

        public void OverwriteButtonPressed()
        {

        }
    }
}