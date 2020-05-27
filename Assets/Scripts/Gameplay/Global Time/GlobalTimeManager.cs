using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTimeManager : MonoBehaviour
{
    public GameObject skyboxObject;
    
    public Color newSkyColor;
    public Color newHorizonColor;
    public Color newCloudsColor;
    public realTime realTimeManager;
    
    public AudioClip[] belllsClip;
    private float [] timingClips;

    private Color initSkyColor;
    private Color initHorizonColor;
    private Color initCloudsColor;

    private Color lerpedSkyColor;
    private Color lerpedHorizonColor;
    private Color lerpedCloudsColor;

    private float lerpValue;

    private float timer;
    private AudioSource audioSource;
    [HideInInspector]
    public bool isNight;

    private int indexBells;
    private float executionTimeMins;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        
        initSkyColor = skyboxObject.GetComponent<MeshRenderer>().material.GetColor("_SkyColor");
        initHorizonColor = skyboxObject.GetComponent<MeshRenderer>().material.GetColor("_HorizonColor");
        initCloudsColor = skyboxObject.GetComponent<MeshRenderer>().material.GetColor("_NubesColor");
        audioSource = GetComponent<AudioSource>();
        timingClips = new float[belllsClip.Length];

        for (int i = 0; i < timingClips.Length; i++)
        {
            timingClips[i] = 120 + (2*60*i);
            
        }

        /*for (int i = 0; i < timingClips.Length; i++)
        {
            timingClips[i] = 5 + (5*i);
            
        }*/

        lerpValue = 0;
        timer = 0;
        isNight = false;
        indexBells = 0;
        executionTimeMins = timingClips[timingClips.Length-1];    //10f
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //Debug.Log(timingClips[indexBells]);

        if (!isNight)
        {
            if (lerpValue < 1)
            {
                lerpValue = timer / (executionTimeMins * 60);
                //lerpValue = timer / (executionTimeMins);

                lerpedSkyColor = Color.Lerp(initSkyColor, newSkyColor, lerpValue);
                lerpedHorizonColor = Color.Lerp(initHorizonColor, newHorizonColor, lerpValue);
                lerpedCloudsColor = Color.Lerp(initCloudsColor, newCloudsColor, lerpValue);
                
                skyboxObject.GetComponent<MeshRenderer>().material.SetColor("_SkyColor", lerpedSkyColor);
                skyboxObject.GetComponent<MeshRenderer>().material.SetColor("_HorizonColor", lerpedHorizonColor);
                skyboxObject.GetComponent<MeshRenderer>().material.SetColor("_NubesColor", lerpedCloudsColor);
            }

            else
            {
                isNight = true;
                realTimeManager.onTime = false;
                gameManager.DeactivatePhysicSun();
                gameManager.PlayGuillotine();
            }
            
            
        }

        if (timer > timingClips[indexBells] && !isNight)
        {
            audioSource.clip = belllsClip[indexBells];
            audioSource.Play();
            if (indexBells < timingClips.Length -1)
            {
                indexBells++;
            }
            
        }
    }

    public void SunOff()
    {
        skyboxObject.GetComponent<MeshRenderer>().material.SetColor("_SkyColor", newSkyColor);
        skyboxObject.GetComponent<MeshRenderer>().material.SetColor("_HorizonColor", newHorizonColor);
        skyboxObject.GetComponent<MeshRenderer>().material.SetColor("_NubesColor", newCloudsColor);
    }
}
