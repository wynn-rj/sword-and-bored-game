using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunctions : MonoBehaviour
{
    public GameObject optionsCanvas, creditsCanvas;
    public GameObject confirmQuitPanel;
    public void StartButtonPressed()
    {       
        SceneManager.LoadScene("PauseCreationScene");
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
