using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;


public class ManagerMenu : MonoBehaviour
{
    public GameObject canvasMenu;
    public GameObject panelPause;
    public GameObject panelOptions;
    public GameObject panelAskExit;
    public GameManager gameManager;

    [HideInInspector]
    public bool paused;
    private FirstPersonController firstPersonController;

    //Singleton property
    public static ManagerMenu Instance {get; private set;}

    private OptionsManager optionsManager;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
        optionsManager = GetComponent<OptionsManager>();
    }

    
    
    // Start is called before the first frame update
    void Start()
    {
        canvasMenu.SetActive(false);
        panelPause.SetActive(false);
        panelOptions.SetActive(false);
        panelAskExit.SetActive(false);

        paused = false;      //true


        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        firstPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();

        firstPersonController.paused = paused;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && gameManager.presentationDone)
        {
            Pause();
        }
    }

    void Pause()
    {
        if (!paused)
        {
            paused = true;
            canvasMenu.SetActive(true);
            panelPause.SetActive(true);
            //Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (DialogueManager.Instance.audioSource.isPlaying)
            {
                DialogueManager.Instance.audioSource.Pause();
            }
            
            

        }

        else if (panelPause.activeSelf)
        {
            paused = false;
            canvasMenu.SetActive(false);
            panelPause.SetActive(false);
            //Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (DialogueManager.Instance.audioSource.clip != null)
            {
                DialogueManager.Instance.audioSource.Play();
            }
        }

        firstPersonController.paused = paused;
    }

    public void Toggle_Changed (bool newValue)
    {
        DialogueManager.Instance.subtitlesEnabled = newValue;
        PlayerPrefs.SetInt("subtitlesEnabled", newValue?1:0);
    }

    public void OpenOptions()
    {
        panelPause.SetActive(false);
        panelOptions.SetActive(true);
    }

    public void CloseOptions()
    {
        panelPause.SetActive(true);
        panelOptions.SetActive(false);
        PlayerPrefs.SetFloat("volume", optionsManager.sliderVolume.value);
        PlayerPrefs.SetInt("quality", optionsManager.dropdownQuality.value);
        PlayerPrefs.SetInt("fullscreen", optionsManager.toggleFullscreen.isOn?1:0);
    }

    public void AskExit()
    {
        panelPause.SetActive(false);
        panelAskExit.SetActive(true);

    }

    public void NoExit()
    {
        panelPause.SetActive(true);
        panelAskExit.SetActive(false);

    }

    public void Exit()
    {
        SceneManager.LoadScene("Init");

    }
}
