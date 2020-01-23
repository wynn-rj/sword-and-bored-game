using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseFunctions : MonoBehaviour
{
    public GameObject pauseCanvas;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseCanvas.SetActive(!pauseCanvas.activeSelf);
        }
    }

    public void ResumePressed()
    {
        pauseCanvas.SetActive(false);
    }

    public void ReturnMainPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
