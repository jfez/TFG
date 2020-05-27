using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeEscape : MonoBehaviour
{
    [HideInInspector]
    public bool locked;
    private bool closing;

    public GameObject wallBehind;

    public AudioClip openDoorClip;
    public AudioClip closeDoorClip;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        locked = false;
        audioSource = GetComponent<AudioSource>();
        closing = false;
    }

    // Update is called once per frame
    void Update()
    {  
        
    }

    void OnMouseOver()
    {
        if (locked)
        {
            if (Input.GetMouseButton(0))
            {
                wallBehind.SetActive(false);

                if (!closing)
                {
                    audioSource.clip = openDoorClip;
                    audioSource.Play();
                    closing = true;
                }
            }

            else
            {
                wallBehind.SetActive(true);

                if (closing)
                {
                    audioSource.clip = closeDoorClip;
                    audioSource.Play();
                    closing = false;
                }
                
            }
        }
    }

    void OnMouseExit()
    {
        wallBehind.SetActive(true);

        if (closing)
        {
            audioSource.clip = closeDoorClip;
            audioSource.Play();
            closing = false;
        }
        
    }

    void OnEnable () 
    {
        TriggerMaze.LockedUp += ActivateLocked;
    }

    void OnDisable () 
    {
        TriggerMaze.LockedUp -= ActivateLocked;
    }

    void ActivateLocked () 
    {
        locked = true;
    }
}
