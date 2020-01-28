using UnityEngine;
using UnityEngine.SceneManagement;

namespace SwordAndBored.UI.MenuFunctions
{
    public class PauseFunctions : MonoBehaviour
    {
        public GameObject pauseCanvas, optionsCanvas;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (optionsCanvas.activeSelf)
                {
                    pauseCanvas.SetActive(true);
                    optionsCanvas.SetActive(false);
                }
                else
                {
                    pauseCanvas.SetActive(!pauseCanvas.activeSelf);
                }
                if (pauseCanvas.activeSelf || optionsCanvas.activeSelf)
                {

                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = 1.0f;
                }
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                string name = SceneManager.GetActiveScene().name;
                if (name.Equals("CameraAddingScene"))
                {
                    SceneManager.LoadScene("ProceduralGenerationTesting");
                } else
                {
                    SceneManager.LoadScene("CameraAddingScene");
                }
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

        public void SaveGamePressed()
        {
            Debug.Log("Save game");
        }

        public void LoadGamePressed()
        {
            Debug.Log("Load Game");
        }

        public void OptionsPressed()
        {
            optionsCanvas.SetActive(true);
            pauseCanvas.SetActive(false);
        }
    }

}