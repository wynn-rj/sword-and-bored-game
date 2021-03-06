﻿using UnityEngine;
using UnityEngine.SceneManagement;
using SwordAndBored.SceneManagement;
using SwordAndBored.GameData.Database;

namespace SwordAndBored.UI.MenuFunctions
{
    public class PauseFunctions : MonoBehaviour
    {
        public GameObject pauseCanvas, optionsCanvas, saveCanvas;

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


        }

        public void HotKeys()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                string name = SceneManager.GetActiveScene().name;
                if (name.Equals(GameScenes.BASEVIEW))
                {
                    SceneManager.LoadScene(GameScenes.STRATEGYMAP);
                }
                else
                {
                    SceneManager.LoadScene(GameScenes.BASEVIEW);
                }
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                SceneManager.LoadScene(GameScenes.BATTLEFIELD);
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                SceneManager.LoadScene("DatabaseTesting");
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                //DatabaseHelper.ClearAllUnitsExceptOneFromUnitTable();
            }
        }

        public void ResumePressed()
        {
            pauseCanvas.SetActive(false);
            Time.timeScale = 1.0f;
        }

        public void ReturnMainPressed()
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(GameScenes.MAINMENU);
        }

        public void SaveGamePressed()
        {
            //Debug.Log("Save game");
            pauseCanvas.SetActive(false);
            saveCanvas.SetActive(true);
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