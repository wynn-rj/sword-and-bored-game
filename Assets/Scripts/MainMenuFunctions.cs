using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunctions : MonoBehaviour
{
    /*private void Start()
    {
       SceneManager.LoadSceneAsync("SampleScene");
    }*/
    public void StartButtonPressed()
    {       
        Debug.Log("Pressed Start");
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitButtonPressed()
    {
        Debug.Log("Pressed Exit Game");
        Application.Quit();
    }
}
