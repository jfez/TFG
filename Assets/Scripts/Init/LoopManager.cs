using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopManager : MonoBehaviour
{
    public Button playButton;
    private AudioSource audioSource;
    private float timePlaying;
    private bool timeRunning;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timePlaying = 0f;
        timeRunning = false;
        
        if (!PlayerPrefs.HasKey("Loop"))
        {
            PlayerPrefs.SetInt("Loop", 0);
        }

        //Loop is broken
        if (PlayerPrefs.GetInt("Loop") != 0)
        {
            playButton.interactable = false;
            audioSource.Play();
            timeRunning = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRunning)
        {
            if (timePlaying <= audioSource.clip.length)
            {
                timePlaying += Time.deltaTime;
            }

            else
            {
                Debug.Log("QUIT");
                Application.Quit();
            }
            
        }
        
        //Technical shortcut to play again even with the loop broken
        if (Input.GetKey(KeyCode.F1) && Input.GetKey(KeyCode.F2))
        {
            if (PlayerPrefs.GetInt("Loop") != 0)
            {
                playButton.interactable = true;
                audioSource.Stop();
                timeRunning = false;
                PlayerPrefs.SetInt("Loop", 0);
            }
        }
    }
}
