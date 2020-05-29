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
    private AudioSource scratchAudioSource;
    private int indexPress;
    private float timeOffset;
    public AudioClip dialogueHermes;
    public AudioClip dialogueEuridice;

    // Start is called before the first frame update
    void Start()
    {
        scratchAudioSource = GameObject.FindGameObjectWithTag("Scratch").GetComponent<AudioSource>();
        locked = false;
        audioSource = GetComponent<AudioSource>();
        closing = false;
        indexPress = 0;
        timeOffset = 2f;
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
                    indexPress++;

                    if (DialogueManager.Instance.audioKindEnum != AudioKind.AudioKindEnum.Event)
                    {
                        if (indexPress == 3)
                        {
                            if (DialogueManager.Instance.audioKindEnum == AudioKind.AudioKindEnum.Time)
                            {
                                scratchAudioSource.gameObject.GetComponent<ScratchManager>().ScratchTime();
                            }

                            else if (DialogueManager.Instance.audioKindEnum == AudioKind.AudioKindEnum.Voxophone)
                            {
                                scratchAudioSource.gameObject.GetComponent<ScratchManager>().ScratchVoxophone();
                            }

                            DialogueManager.Instance.audioSource.spatialBlend = 0f;
                            DialogueManager.Instance.BeginDialogue(dialogueHermes, timeOffset, AudioKind.AudioKindEnum.Event);
                        }

                        else if (indexPress == 9)
                        {
                            if (DialogueManager.Instance.audioKindEnum == AudioKind.AudioKindEnum.Time)
                            {
                                scratchAudioSource.gameObject.GetComponent<ScratchManager>().ScratchTime();
                            }

                            else if (DialogueManager.Instance.audioKindEnum == AudioKind.AudioKindEnum.Voxophone)
                            {
                                scratchAudioSource.gameObject.GetComponent<ScratchManager>().ScratchVoxophone();
                            }

                            DialogueManager.Instance.audioSource.spatialBlend = 0f;
                            DialogueManager.Instance.BeginDialogue(dialogueEuridice, timeOffset, AudioKind.AudioKindEnum.Event);
                        }
                        
                        
                    }                 
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
