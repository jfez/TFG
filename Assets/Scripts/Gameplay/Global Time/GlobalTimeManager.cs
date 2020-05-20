using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTimeManager : MonoBehaviour
{
    public GameObject skyboxObject;
    
    public Color newSkyColor;
    public Color newHorizonColor;
    public Color newCloudsColor;
    public GameObject physicalSun;
    
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


    // Start is called before the first frame update
    void Start()
    {
        initSkyColor = skyboxObject.GetComponent<MeshRenderer>().material.GetColor("_SkyColor");
        initHorizonColor = skyboxObject.GetComponent<MeshRenderer>().material.GetColor("_HorizonColor");
        initCloudsColor = skyboxObject.GetComponent<MeshRenderer>().material.GetColor("_NubesColor");
        audioSource = GetComponent<AudioSource>();
        timingClips = new float[belllsClip.Length];

        for (int i = 0; i < timingClips.Length; i++)
        {
            if (i == 0)
            {
                timingClips[i] = 60;
            }

            else
            {
                timingClips[i] = 60 + (3*60*i);
            }
            
        }

        lerpValue = 0;
        timer = 0;
        isNight = false;
        indexBells = 0;
        executionTimeMins = 13f;

        physicalSun.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (lerpValue < 1 && !isNight)
        {
            lerpValue = timer / (executionTimeMins * 60);
            //Debug.Log(lerpValue);

            lerpedSkyColor = Color.Lerp(initSkyColor, newSkyColor, lerpValue);
            lerpedHorizonColor = Color.Lerp(initHorizonColor, newHorizonColor, lerpValue);
            lerpedCloudsColor = Color.Lerp(initCloudsColor, newCloudsColor, lerpValue);
            
            skyboxObject.GetComponent<MeshRenderer>().material.SetColor("_SkyColor", lerpedSkyColor);
            skyboxObject.GetComponent<MeshRenderer>().material.SetColor("_HorizonColor", lerpedHorizonColor);
            skyboxObject.GetComponent<MeshRenderer>().material.SetColor("_NubesColor", lerpedCloudsColor);
        }
        
        
        /*
        if (Input.GetKey(KeyCode.F2) && lerpValue <= 1)
        {
            lerpValue += 0.01f;

            Debug.Log(lerpValue);
            
            lerpedSkyColor = Color.Lerp(initSkyColor, newSkyColor, lerpValue);
            lerpedHorizonColor = Color.Lerp(initHorizonColor, newHorizonColor, lerpValue);
            lerpedCloudsColor = Color.Lerp(initCloudsColor, newCloudsColor, lerpValue);
            
            skyboxObject.GetComponent<MeshRenderer>().material.SetColor("_SkyColor", lerpedSkyColor);
            skyboxObject.GetComponent<MeshRenderer>().material.SetColor("_HorizonColor", lerpedHorizonColor);
            skyboxObject.GetComponent<MeshRenderer>().material.SetColor("_NubesColor", lerpedCloudsColor);
            
        }
        */

        if (Input.GetKey(KeyCode.F3))
        {
            physicalSun.SetActive(true);
        }

        if (timer > timingClips[indexBells])
        {
            audioSource.clip = belllsClip[indexBells];
            audioSource.Play();
            indexBells++;
        }
    }

    public void SunOff()
    {
        skyboxObject.GetComponent<MeshRenderer>().material.SetColor("_SkyColor", newSkyColor);
        skyboxObject.GetComponent<MeshRenderer>().material.SetColor("_HorizonColor", newHorizonColor);
        skyboxObject.GetComponent<MeshRenderer>().material.SetColor("_NubesColor", newCloudsColor);
    }
}
