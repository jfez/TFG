using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenerSculpture : MonoBehaviour
{
    private Transform Eyes;
    
    private VoiceRecognition voiceRecognition;
    private float listenerDistance;
    float lerpedValue;
    
    // Start is called before the first frame update
    void Start()
    {
        Eyes = GameObject.FindGameObjectWithTag("MainCamera").transform;
        voiceRecognition = GameObject.FindGameObjectWithTag("VoiceRecognition").GetComponent<VoiceRecognition>();
        listenerDistance = 7f;
        lerpedValue = 0;
        GetComponent<Renderer>().material.SetFloat("_Metallic", lerpedValue);
        //Debug.Log(lerpedColor);
        //Debug.Log(GetComponent<Renderer>().material.color);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(Eyes.position, transform.position) < listenerDistance)
        {
            if (!ManagerMenu.Instance.paused)
            {
                //Debug.Log(voiceRecognition.GetLoudness());
                float percentage = voiceRecognition.GetLoudness() / 15;
                //Debug.Log(percentage);
                lerpedValue = percentage;
                //Debug.Log(lerpedColor);
                GetComponent<Renderer>().material.SetFloat("_Metallic", lerpedValue);
            } 
        }

        else
        {
            //float percentage = 0;
            lerpedValue = 0;
            GetComponent<Renderer>().material.SetFloat("_Metallic", lerpedValue);
        }
    }
}
