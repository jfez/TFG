using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


public class ManagerMenu : MonoBehaviour
{
    public GameObject canvasMenu;

    public bool paused;
    private FirstPersonController firstPersonController;

    //Singleton property
    public static ManagerMenu Instance {get; private set;}

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
        
    }

    
    
    // Start is called before the first frame update
    void Start()
    {
        canvasMenu.SetActive(false);
        paused = false;
        Time.timeScale = 1f;

        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        firstPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();

        firstPersonController.paused = paused;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
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
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (DialogueManager.Instance.audioSource.isPlaying)
            {
                DialogueManager.Instance.audioSource.Pause();
            }
            
            

        }

        else
        {
            paused = false;
            canvasMenu.SetActive(false);
            Time.timeScale = 1f;
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
    }
}
