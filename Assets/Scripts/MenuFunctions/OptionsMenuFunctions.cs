using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

namespace SwordAndBored.UI.MenuFunctions
{

    public class OptionsMenuFunctions : MonoBehaviour
    {
        public GameObject previousCanvas;
        public AudioMixer audioMixer;
        public Slider volumeSlider;
        public TMP_Dropdown qualityDropdown;
        public Toggle fullscreenToggle;
        public TMP_Dropdown resolutionDropdown;
        public Resolution[] resolutions;

        void Start()
        {
            /* Default Resolution Settings */
            int currentHeight = PlayerPrefs.GetInt("savedHeight", Screen.currentResolution.height);
            int currentWidth = PlayerPrefs.GetInt("savedWidth", Screen.currentResolution.width);
            int currentRefreshRate = PlayerPrefs.GetInt("savedRefreshRate", Screen.currentResolution.refreshRate);
            resolutions = Screen.resolutions;

            resolutionDropdown.ClearOptions();
            List<string> options = new List<string>();
            int currentResIndex = 5;
            for (int i = 0; i < resolutions.Length; i++)
            {
                options.Add(resolutions[i].width + "X" + resolutions[i].height + " (" + resolutions[i].refreshRate + "Hz)");

                if (resolutions[i].height == currentHeight && resolutions[i].width == currentWidth && resolutions[i].refreshRate == currentRefreshRate)
                {
                    currentResIndex = i;
                }
            }
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResIndex;
            resolutionDropdown.RefreshShownValue();

            /* Default Volume Settings */
            float savedVolume = PlayerPrefs.GetFloat("masterVolume", 0);
            volumeSlider.value = savedVolume;
            audioMixer.SetFloat("masterVolume", savedVolume);

            /* Default Fullscreen Settings */
            fullscreenToggle.isOn = Screen.fullScreen;
            
            /* Default Quality Settings */
            qualityDropdown.value = QualitySettings.GetQualityLevel();
            qualityDropdown.RefreshShownValue();

        }

        public void BackButtonPressed()
        {
            previousCanvas.SetActive(true);
            gameObject.SetActive(false);
        }

        public void SetVolume(float volumeValue)
        {
            audioMixer.SetFloat("masterVolume", volumeValue);
            PlayerPrefs.SetFloat("masterVolume", volumeValue);
            PlayerPrefs.Save();
        }

        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }

        public void SetFullscreen(bool isFullScreen)
        {
            Screen.fullScreen = isFullScreen;
        }

        public void SetResolution(int resIndex)
        {
            Screen.SetResolution(resolutions[resIndex].width, resolutions[resIndex].height, Screen.fullScreen);
            PlayerPrefs.SetInt("savedHeight", resolutions[resIndex].height);
            PlayerPrefs.SetInt("savedWidth", resolutions[resIndex].width);
            PlayerPrefs.SetInt("savedRefreshRate", resolutions[resIndex].refreshRate);
        }
    }

}