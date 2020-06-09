using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitManager : MonoBehaviour
{
    public GameObject panelGeneral;
    public GameObject panelOptions;

    public GameObject panelCredits;
    public GameObject panelMicroConfig;
    public GameObject textCredits;
    private OptionsManager optionsManager;

    public Dropdown dropdownMicrophone;
    public VRConfig vRConfig;

    private string[] microphones;
    
    // Start is called before the first frame update
    void Start()
    {

        dropdownMicrophone.ClearOptions();

        List<string> options = new List<string>();

        
        for (int i = 0; i < Microphone.devices.Length; i++)
        {
            string option = Microphone.devices[i].ToString();
            options.Add(option);
        }

        dropdownMicrophone.AddOptions(options);
        //dropdownMicrophone.RefreshShownValue();
        
        panelGeneral.SetActive(true);
        panelOptions.SetActive(false);
        panelCredits.SetActive(false);
        panelMicroConfig.SetActive(false);
        optionsManager = GetComponent<OptionsManager>();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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

    public void MicroConfig()
    {
        panelGeneral.SetActive(false);
        panelMicroConfig.SetActive(true);
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

    public void BackFromMicroConfig()
    {
        panelGeneral.SetActive(true);
        panelMicroConfig.SetActive(false);
    }

    public void SetMicrophone(int microphoneIndex)
    {
        //Debug.Log(microphoneIndex);
        vRConfig.RestartMicrophone(microphoneIndex);
    }


}
