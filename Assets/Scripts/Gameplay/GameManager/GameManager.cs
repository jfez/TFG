﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    //Singleton property
    public static GameManager Instance {get; private set;}
    
    [HideInInspector]
    public int indexIsland;
    [HideInInspector]
    public int indexInsideIsland;

    public AudioSource audioScreams;
    public GameObject colliderSecondPortal;
    public GameObject colliderSecondStop;

    public GameObject colliderThirdPortal;
    public GameObject colliderThirdStop;
    public GlobalTimeManager globalTimeManager;
    public GameObject physicalSun;
    public realTime realTimeManager;

    public AudioClip initClip;
    public AudioSource BSOAudioSource;

    private float timer;
    private bool fadeOut;
    [HideInInspector]
    public bool presentationDone;

    public Image img;

    private FirstPersonController firstPersonController;

    public CharacterController player;
    public Transform beginPoint;

    public AudioClip[] arrayAudiosCorridor;
    public float[] arrayTimesCorridor;

    public AudioClip[] arrayAudiosInitialIsland;
    public float[] arrayTimesInitialIsland;
    public AudioClip[] arrayAudiosStatuesIsland;
    public float[] arrayTimesStatuesIsland;

    public AudioClip[] arrayAudiosTimeIslandDay;
    public float[] arrayTimesTimeIslandDay;

    public AudioClip[] arrayAudiosTimeIslandNight;
    public float[] arrayTimesTimeIslandNight;
    
    [HideInInspector]
    public float timerAudios;
    [HideInInspector]
    public int indexAudio;
    [HideInInspector]
    public bool finishAudios;
    public AudioSource guillotineAudio;
    public GuillotineManager guillotineManager;
    [HideInInspector]
    public bool manualNight;
    
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
        indexIsland = 1;
        indexInsideIsland = 1;
        indexAudio = 0;
        physicalSun.SetActive(false);

        timer = 0;
        timerAudios = 0;
        fadeOut = true;            //false
        presentationDone = true;   //false
        finishAudios = false;

        BSOAudioSource.clip = initClip;
        //BSOAudioSource.Play();
        img.gameObject.SetActive(false);    //quitar

        
        firstPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();

        player.enabled = false;
        player.transform.position = beginPoint.position;
        player.enabled = true;
        manualNight = false;



    }

    // Update is called once per frame
    void Update()
    {
        /*if (timer < initClip.length - 5)
        {
            timer += Time.deltaTime;
        }

        else
        {
            if (!fadeOut)
            {
                StartCoroutine(FadeOut());
                fadeOut = true;
            }
        }*/

        if (!ManagerMenu.Instance.paused && !finishAudios && presentationDone)
        {
            timerAudios += Time.deltaTime;
            //Debug.Log(indexAudio);
            //Debug.Log(timerAudios);
        }

        if (indexIsland == 1 && indexInsideIsland == 1)     //corridor
        {
            if(timerAudios > arrayTimesCorridor[indexAudio])
            {
                DialogueManager.Instance.audioSource.spatialBlend = 0f;
                DialogueManager.Instance.BeginDialogue(arrayAudiosCorridor[indexAudio], 2f, AudioKind.AudioKindEnum.Time);
                if (indexAudio < arrayTimesCorridor.Length - 1)
                {
                    indexAudio++;
                }

                else
                {
                    finishAudios = true;
                    timerAudios = 0;
                }
                
            }

            if (DialogueManager.Instance.audioKindEnum != AudioKind.AudioKindEnum.None)
            {
                timerAudios = 0;
            }
        }

        else if (indexIsland == 1 && indexInsideIsland == 2)        //initial island
        {
            if(timerAudios > arrayTimesInitialIsland[indexAudio])
            {
                DialogueManager.Instance.audioSource.spatialBlend = 0f;
                DialogueManager.Instance.BeginDialogue(arrayAudiosInitialIsland[indexAudio], 2f, AudioKind.AudioKindEnum.Time);
                if (indexAudio < arrayTimesInitialIsland.Length - 1)
                {
                    indexAudio++;
                }

                else
                {
                    finishAudios = true;
                    timerAudios = 0;
                }
                
            }

            if (DialogueManager.Instance.audioKindEnum != AudioKind.AudioKindEnum.None)
            {
                timerAudios = 0;
            }
        }

        else if (indexIsland == 2 && indexInsideIsland == 1)    //statues island 
        {
            if(timerAudios > arrayTimesStatuesIsland[indexAudio])
            {
                DialogueManager.Instance.audioSource.spatialBlend = 0f;
                DialogueManager.Instance.BeginDialogue(arrayAudiosStatuesIsland[indexAudio], 2f, AudioKind.AudioKindEnum.Time);
                
                indexAudio = Random.Range(1,4);
                
            }

            if (DialogueManager.Instance.audioKindEnum != AudioKind.AudioKindEnum.None)
            {
                timerAudios = 0;
            }
        }

        /*else if (indexIsland == 3)      //time island
        {
            if (GetIsNight() && indexInsideIsland != 2 && !manualNight)
            {
                indexInsideIsland = 2;
                finishAudios = false;
                timerAudios = 0;
                indexAudio = 0;
            }

            if (indexInsideIsland == 1)     //day
            {
                if (manualNight && !finishAudios)
                {
                    finishAudios = true;
                    timerAudios = 0;
                }
                
                if(timerAudios > arrayTimesTimeIslandDay[indexAudio])
                {
                    DialogueManager.Instance.audioSource.spatialBlend = 0f;
                    DialogueManager.Instance.BeginDialogue(arrayAudiosTimeIslandDay[indexAudio], 2f, AudioKind.AudioKindEnum.Time);
                    
                    if (indexAudio < arrayTimesTimeIslandDay.Length - 1)
                    {
                        indexAudio++;
                    }

                    else
                    {
                        finishAudios = true;
                        timerAudios = 0;
                    }  
                }            
            }

            else        // automatic night (execution done)
            {
                if(timerAudios > arrayTimesTimeIslandNight[indexAudio])
                {
                    DialogueManager.Instance.audioSource.spatialBlend = 0f;
                    DialogueManager.Instance.BeginDialogue(arrayAudiosTimeIslandNight[indexAudio], 2f, AudioKind.AudioKindEnum.Time);
                    
                    if (indexAudio < arrayTimesTimeIslandNight.Length - 1)
                    {
                        indexAudio++;
                    }

                    else
                    {
                        finishAudios = true;
                        timerAudios = 0;
                    }  
                }   
            }

            if (DialogueManager.Instance.audioKindEnum != AudioKind.AudioKindEnum.None)
            {
                timerAudios = 0;
            }
        }*/ 


    }

    public void ActivateScreams()
    {
        audioScreams.Play();
    }

    public void OpenSecondPortal()
    {
        if (!colliderSecondPortal.activeSelf)
        {
            colliderSecondPortal.SetActive(true);
            colliderSecondStop.SetActive(false);
        }
    }

    public void OpenThirdPortal()
    {

        colliderThirdPortal.SetActive(true);
        colliderThirdStop.SetActive(false);
        
    }

    public void CloseThirdPortal()
    {
        colliderThirdPortal.SetActive(false);
        colliderThirdStop.SetActive(true);
        
    }

    public void ActivatePhysicSun()
    {
        physicalSun.SetActive(true);
    }
    public void DeactivatePhysicSun()
    {
        realTimeManager.running = false;
        physicalSun.SetActive(false);
    }

    public bool GetIsNight()
    {
        return globalTimeManager.isNight;
    }

    private IEnumerator FadeOut()
    {
        // loop over 3 seconds backwards
        for (float i = 3; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            img.color = new Color(0, 0, 0, i);
            yield return null;
        }

        presentationDone = true;
        firstPersonController.paused = false;
        ManagerMenu.Instance.paused = false;
    }

    public void PlayGuillotine()
    {
        
        StartCoroutine(GuillotineSound());
    }

    private IEnumerator GuillotineSound()
    {
        yield return new WaitForSeconds(5f);
        guillotineManager.Execution();
        guillotineAudio.Play();
    }
}
