using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitManager : MonoBehaviour
{
    public GameObject panelGeneral;
    public GameObject panelOptions;

    public GameObject panelCredits;
    public GameObject textCredits;
    private OptionsManager optionsManager;
    
    // Start is called before the first frame update
    void Start()
    {
        panelGeneral.SetActive(true);
        panelOptions.SetActive(false);
        panelCredits.SetActive(false);
        optionsManager = GetComponent<OptionsManager>();
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
        panelGeneral.SetActive(false);
        panelOptions.SetActive(true);
    }

    public void Credits()
    {
        panelGeneral.SetActive(false);
        panelCredits.SetActive(true);
        textCredits.GetComponent<Animator>().SetTrigger("playCredits");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void BackGeneral()
    {
        panelGeneral.SetActive(true);
        panelOptions.SetActive(false);
        PlayerPrefs.SetFloat("volume", optionsManager.sliderVolume.value);
        PlayerPrefs.SetInt("quality", optionsManager.dropdownQuality.value);
        PlayerPrefs.SetInt("fullscreen", optionsManager.toggleFullscreen.isOn?1:0);

    }

    public void BackFromCredits()
    {
        panelGeneral.SetActive(true);
        panelCredits.SetActive(false);
    }


}
