using UnityEngine;
using UnityEngine.SceneManagement;

namespace SwordAndBored.UI.MenuFunctions
{
    public class MainMenuFunctions : MonoBehaviour
    {
        public GameObject optionsCanvas, creditsCanvas, loadingCanvas;
        public GameObject confirmQuitPanel;

        public void StartButtonPressed()
        {
            loadingCanvas.SetActive(true);
            gameObject.SetActive(false);
            //SceneManager.LoadScene("PauseCreationScene");
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