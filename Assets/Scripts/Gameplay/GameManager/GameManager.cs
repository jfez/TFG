using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    public static int indexIsland;

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
    
    // Start is called before the first frame update
    void Start()
    {
        indexIsland = 1;
        physicalSun.SetActive(false);

        timer = 0;
        fadeOut = true;            //false
        presentationDone = true;   //false

        BSOAudioSource.clip = initClip;
        //BSOAudioSource.Play();
        img.gameObject.SetActive(false);    //quitar

        
        firstPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();

        player.enabled = false;
        //player.transform.position = beginPoint.position;
        player.enabled = true;



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
}
