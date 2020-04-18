using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTimeManager : MonoBehaviour
{
    public GameObject skyboxObject;
    
    public Color newSkyColor;
    public Color newHorizonColor;
    public Color newCloudsColor;

    private Color initSkyColor;
    private Color initHorizonColor;
    private Color initCloudsColor;

    private Color lerpedSkyColor;
    private Color lerpedHorizonColor;
    private Color lerpedCloudsColor;

    private float lerpValue;

    // Start is called before the first frame update
    void Start()
    {
        initSkyColor = skyboxObject.GetComponent<MeshRenderer>().material.GetColor("_SkyColor");
        initHorizonColor = skyboxObject.GetComponent<MeshRenderer>().material.GetColor("_HorizonColor");
        initCloudsColor = skyboxObject.GetComponent<MeshRenderer>().material.GetColor("_NubesColor");

        lerpValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
