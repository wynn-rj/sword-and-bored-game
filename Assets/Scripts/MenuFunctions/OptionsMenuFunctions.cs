using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsMenuFunctions : MonoBehaviour
{
    public GameObject previousCanvas;
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public TMP_Dropdown resolutionDropdown;
    public Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResIndex = 0;
        for (int i =0; i<resolutions.Length; i++)
        {
            options.Add(resolutions[i].width + "X" + resolutions[i].height + " (" + resolutions[i].refreshRate + "Hz)");

            if (resolutions[i].height == Screen.currentResolution.height && resolutions[i].width == Screen.currentResolution.width)
            {
                currentResIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();

        float currentVolume;
        audioMixer.GetFloat("masterVolume", out currentVolume);
        volumeSlider.value = currentVolume;
    }

    public void BackButtonPressed()
    {
        previousCanvas.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SetVolume(float volumeValue)
    {
        audioMixer.SetFloat("masterVolume", volumeValue);
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
    }
}
