using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenerSculptureConfig : MonoBehaviour
{
    public VRConfig voiceRecognition;

    float lerpedValue;
    
    // Start is called before the first frame update
    void Start()
    {
        lerpedValue = 0;
        GetComponent<Renderer>().material.SetFloat("_Metallic", lerpedValue);

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(voiceRecognition.GetLoudness());
        float percentage = voiceRecognition.GetLoudness() / 15;
        //Debug.Log(percentage);
        lerpedValue = percentage;
        //Debug.Log(lerpedColor);
        GetComponent<Renderer>().material.SetFloat("_Metallic", lerpedValue);
    }
}
