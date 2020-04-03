using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider sliderVolume;
    public Dropdown dropdownQuality;
    public Dropdown dropdownResolutions;
    public Toggle toggleFullscreen;

    private Resolution[] resolutions;
    
    void Start()
    {
        resolutions = Screen.resolutions;
        dropdownResolutions.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        dropdownResolutions.AddOptions(options);
        dropdownResolutions.value = currentResolutionIndex;
        dropdownResolutions.RefreshShownValue();

        
        if (PlayerPrefs.HasKey("volume"))
        {
            sliderVolume.value = PlayerPrefs.GetFloat("volume");
        }

        else
        {
            PlayerPrefs.SetFloat("volume", sliderVolume.value);
        }

        if (PlayerPrefs.HasKey("quality"))
        {
            dropdownQuality.value = PlayerPrefs.GetInt("quality");
        }

        else
        {
            PlayerPrefs.SetInt("quality", dropdownQuality.value);
        }

        if (PlayerPrefs.HasKey("fullscreen"))
        {
            toggleFullscreen.isOn = PlayerPrefs.GetInt("fullscreen")==1?true:false;
        }

        else
        {
            PlayerPrefs.SetInt("fullscreen", toggleFullscreen.isOn?1:0);
        }

        ChangeMixer(PlayerPrefs.GetFloat("volume"));
        SetQuality(PlayerPrefs.GetInt("quality"));
        SetFullscreen(PlayerPrefs.GetInt("fullscreen")==1?true:false);
    }
    
    public void SetVolume (float volume)
    {
        ChangeMixer(volume);
    }

    void ChangeMixer(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
