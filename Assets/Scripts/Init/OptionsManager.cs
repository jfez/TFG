using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider sliderVolume;
    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            sliderVolume.value = PlayerPrefs.GetFloat("volume");
        }

        else
        {
            PlayerPrefs.SetFloat("volume", sliderVolume.value);
        }

        SetVolume(PlayerPrefs.GetFloat("volume"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
