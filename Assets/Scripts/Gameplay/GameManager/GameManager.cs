using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

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

    public Image imgWhite;
    public Image imgBlack;

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
    private float timerDecision;
    [HideInInspector]
    public bool decision;
    public PressButton plant, fish;
    private float timeDecision;
    private float timerInaction;
    private float timeInaction;

    public VoiceRecognition voiceRecognition;

    [HideInInspector]
    public bool statueDisolved;
    [HideInInspector]
    public bool onPositionToDisolve;
    public GameObject statueSign;
    public Transform particlesDissolvePosition;
    public GameObject particlesDissolveEffect;

    public SaveVoice saveVoice;
    [HideInInspector]
    public bool action;
    public AudioSource fallDownJail;
    public GameObject[] prisoners;
    public LoopManagerGame loopManager;
    public GameObject shaderSecondPortalDisabled;
    public GameObject thirdSecondPortalDisabled;



    
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
        indexIsland = 1;        //1
        indexInsideIsland = 1;
        indexAudio = 0;
        physicalSun.SetActive(false);

        timer = 0;
        timerAudios = 0;
        timerDecision = 0;
        timeDecision = 60;
        timerInaction = 0;
        timeInaction = 90;
        fadeOut = true;            //false
        presentationDone = true;   //false
        finishAudios = false;
        decision = false;
        action = false;
        statueDisolved = false;
        onPositionToDisolve = false;

        BSOAudioSource.clip = initClip;
        //BSOAudioSource.Play();        //outcomment
        imgWhite.gameObject.SetActive(false);    //true
        imgBlack.gameObject.SetActive(false);

        
        firstPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();

        player.enabled = false;
        //player.transform.position = beginPoint.position;    //outcomment
        player.enabled = true;
        manualNight = false;



    }

    // Update is called once per frame
    void Update()
    {
        //outcomment
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

        else if (indexIsland == 3)      //time island
        {
            if (!decision)
            {
                timerDecision += Time.deltaTime;
                if (timerDecision > timeDecision)
                {
                    plant.NoDecision();
                    fish.NoDecision();
                    fish.PlayAudio();
                    decision = true;
                }
            }
            
            
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
        }

        else if (!finishAudios)
        {
            finishAudios = true;
        }

        if (indexIsland == 6)   //lacanian island
        {   
            if (!action)
            {
                timerInaction += Time.deltaTime;
                //Debug.Log(timerInaction);
                if (timerInaction > timeInaction)
                {
                    DisableMovement();
                    loopManager.BreakLoop();
                    StartCoroutine(FadeIn());
                    action = true;
                }
            }
        }

    }

    public void DisableMovement()
    {
        firstPersonController.paused = true;
    }

    public void EnableMovement()
    {
        firstPersonController.paused = false;
    }
    
    public void ActivateScreams()
    {
        audioScreams.Play();
    }

    private void DeactivateScreams()
    {
        audioScreams.Stop();
    }

    public void OpenSecondPortal()
    {
        if (!colliderSecondPortal.activeSelf)
        {
            colliderSecondPortal.SetActive(true);
            colliderSecondStop.SetActive(false);
            DissolvePrisoners();
            shaderSecondPortalDisabled.SetActive(false);
        }
    }

    public void OpenThirdPortal()
    {

        colliderThirdPortal.SetActive(true);
        colliderThirdStop.SetActive(false);
        thirdSecondPortalDisabled.SetActive(false);
        
    }

    public void CloseThirdPortal()
    {
        colliderThirdPortal.SetActive(false);
        colliderThirdStop.SetActive(true);
        thirdSecondPortalDisabled.SetActive(true);
        
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
        for (float i = 3; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            imgWhite.color = new Color(imgWhite.color.r, imgWhite.color.g, imgWhite.color.b, i/3);
            yield return null;
        }

        imgWhite.color = new Color(imgWhite.color.r, imgWhite.color.g, imgWhite.color.b, 0);
        presentationDone = true;
        firstPersonController.paused = false;
        ManagerMenu.Instance.paused = false;
    }

    private IEnumerator FadeIn()
    {
        imgBlack.gameObject.SetActive(true);
        for (float i = 0; i <= 3; i += Time.deltaTime)
        {
            // set color with i as alpha
            imgBlack.color = new Color(imgBlack.color.r, imgBlack.color.g, imgBlack.color.b, i/3);
            yield return null;
        }
        imgBlack.color = new Color(imgBlack.color.r, imgBlack.color.g, imgBlack.color.b, 1);

        
        yield return new WaitForSeconds (2f);
        SceneManager.LoadScene("Init");
    }

    private IEnumerator FadeInAndOut()
    {
        imgWhite.gameObject.SetActive(true);
        
        imgWhite.color = new Color(imgWhite.color.r, imgWhite.color.g, imgWhite.color.b, 1);

        yield return new WaitForSeconds(3f);

        for (float i = 3; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            imgWhite.color = new Color(imgWhite.color.r, imgWhite.color.g, imgWhite.color.b, i/3);
            yield return null;
        }

        imgWhite.color = new Color(imgWhite.color.r, imgWhite.color.g, imgWhite.color.b, 0);
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

    public void DissolveStatueSign()
    {
        StartCoroutine(DissolveStatue(statueSign, false));
        statueSign.GetComponent<BoxCollider>().enabled = false;
        statueDisolved = true;
    }

    public void DissolvePrisoners()
    {
        foreach (GameObject statue in prisoners)
        {
            StartCoroutine(DissolveStatue(statue, true));
        }
    }

    private IEnumerator DissolveStatue(GameObject statue, bool freedSoul)
    {
        float timeDissolve = 3f;
        float lerpValue = 0;
        if (freedSoul)
        {
            GameObject particlesInstanced = Instantiate (particlesDissolveEffect, particlesDissolvePosition.position, particlesDissolveEffect.transform.rotation);
            Destroy(particlesInstanced, 10f);
        }
        
        // loop over 3 seconds backwards
        for (float i = 0; i <= timeDissolve; i += Time.deltaTime)
        {
            lerpValue = i / timeDissolve;
            statue.GetComponent<MeshRenderer>().material.SetFloat("_Mask", lerpValue);
            yield return null;
        }

        Destroy(statue);
    }

    public void PlayScreamsAndPlayer()
    {
        saveVoice.PlayPlayerVoice();
        ActivateScreams();
    }

    public void StopScreamsAndPlayer()
    {
        saveVoice.StopPlayerVoice();
        DeactivateScreams();

    }

    public void PlayFallDownJail()
    {
        fallDownJail.Play();
    }
    public void FinalFade()
    {
        StartCoroutine(FadeIn());
    }
    public void PlatonicFade()
    {
        StartCoroutine(FadeInAndOut());
    }
}
