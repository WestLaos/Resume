using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
public class SettingsMenu : MenuEssentials
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionMenu;
    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionMenu.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        int iter = 0;
        foreach (Resolution curr in resolutions)
        {
            string option = curr.width + " x " + curr.height;
            options.Add(option);

            if (curr.width == Screen.currentResolution.width && curr.height == Screen.currentResolution.height)
            {
                currentResolutionIndex = iter;
            }
            iter++;
        }
        resolutionMenu.AddOptions(options);
        resolutionMenu.value = currentResolutionIndex;
        resolutionMenu.RefreshShownValue();
    }

    public void ChangeResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float sliderValue)
    {
        audioMixer.SetFloat("MainVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
