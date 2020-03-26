﻿using System.Collections.Generic;
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
        // Start is called before the first frame update
        void Start()
        {
            oldFileNames = DatabaseManager.GetPreviousSaveNames();
            if(oldFileNames.Count > 0)
            {
                previousSaves.AddOptions(oldFileNames);
            } else
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
            if (oldFileNames.Contains(fileInput.text)) {
                // Open panel to confirm overwrite
                Debug.Log("File already exists with this name");
            }
            DatabaseManager.SaveData(fileInput.text);
            returnCanvas.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OverwriteButtonPressed()
        {
            DatabaseManager.SaveData(fileInput.text);
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