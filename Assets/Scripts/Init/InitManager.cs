using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InitManager : MonoBehaviour
{
    public GameObject panelGeneral;
    public GameObject panelOptions;
    public Slider sliderVolume;
    public Dropdown dropdownQuality;
    public Toggle toggleFullscreen;
    
    // Start is called before the first frame update
    void Start()
    {
        panelOptions.SetActive(false);
        panelGeneral.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene("Load");
    }

    public void Options()
    {
        panelOptions.SetActive(true);
        panelGeneral.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void BackGeneral()
    {
        panelOptions.SetActive(false);
        panelGeneral.SetActive(true);
        PlayerPrefs.SetFloat("volume", sliderVolume.value);
        PlayerPrefs.SetInt("quality", dropdownQuality.value);
        PlayerPrefs.SetInt("fullscreen", toggleFullscreen.isOn?1:0);
    }


}
