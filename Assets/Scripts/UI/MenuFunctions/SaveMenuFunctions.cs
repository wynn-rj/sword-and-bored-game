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
            previousSaves.AddOptions(oldFilenames);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}