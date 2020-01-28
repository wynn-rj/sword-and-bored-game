using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SwordAndBored.UI.MenuFunctions
{
    public class LoadingFunctions : MonoBehaviour
    {
        public GameObject[] images;
        public Slider loadingSlider;

        private void Start()
        {
            StartCoroutine("CycleImages");
        }

        void LoadGameScene()
        {
            SceneManager.LoadScene("PauseCreationScene");
        }

        IEnumerator CycleImages()
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].SetActive(true);
                loadingSlider.value = (i + 1) * (100.0f / images.Length);
                yield return new WaitForSeconds(1f);
                images[i].SetActive(false);
            }
            LoadGameScene();
        }
    }
}