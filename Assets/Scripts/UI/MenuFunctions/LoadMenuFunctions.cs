using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SwordAndBored.GameData.Database;
using UnityEngine.SceneManagement;

namespace SwordAndBored.UI.MenuFunctions
{
    public class LoadMenuFunctions : MonoBehaviour
    {
        public TMP_Dropdown previousSaves;
        public Button loadButton, cancelButton;
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
            LeaveCanvas();
            SceneManager.LoadScene(SceneManagement.GameScenes.BASEVIEW);
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