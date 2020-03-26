using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using SwordAndBored.GameData.Database;

namespace SwordAndBored.UI.MenuFunctions
{
    public class MainMenuFunctions : MonoBehaviour
    {
        public GameObject optionsCanvas, creditsCanvas;
        public GameObject confirmQuitPanel;

        public AudioMixer audioMixer;

        /*
         * Needed to set default for volume
         */
        private void Start()
        {
            float savedVolume = PlayerPrefs.GetFloat("masterVolume", 0);
            audioMixer.SetFloat("masterVolume", savedVolume);
        }
        public void StartButtonPressed()
        {
            DatabaseManager.LoadFromDefault();
            SceneManager.LoadScene(SceneManagement.GameScenes.BASEVIEW);
        }

        public void LoadButtonPressed()
        {
            // Switch Canvas
        }

        public void ExitButtonPressed()
        {
            confirmQuitPanel.SetActive(true);
        }

        public void OptionsButtonPressed()
        {
            optionsCanvas.SetActive(true);
            gameObject.SetActive(false);
        }

        public void CreditsButtonPressed()
        {
            creditsCanvas.SetActive(true);
            gameObject.SetActive(false);
        }

        public void CancelQuitPressed()
        {
            confirmQuitPanel.SetActive(false);
        }


        public void ConfirmQuitPressed()
        {
            Debug.Log("Pressed Confirm Quit");
            Application.Quit();
        }


    }

}