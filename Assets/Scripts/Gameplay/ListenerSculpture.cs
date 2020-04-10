using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenerSculpture : MonoBehaviour
{
    private Transform Eyes;
    
    private VoiceRecognition voiceRecognition;
    private float listenerDistance;
    Color lerpedColor;
    
    // Start is called before the first frame update
    void Start()
    {
        Eyes = GameObject.FindGameObjectWithTag("MainCamera").transform;
        voiceRecognition = GameObject.FindGameObjectWithTag("VoiceRecognition").GetComponent<VoiceRecognition>();
        listenerDistance = 7f;
        lerpedColor = Color.red;
        GetComponent<Renderer>().material.SetColor("_BaseColor", lerpedColor);
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
                lerpedColor = Color.Lerp(Color.red, Color.blue, percentage);
                //Debug.Log(lerpedColor);
                GetComponent<Renderer>().material.SetColor("_BaseColor", lerpedColor);
            } 
        }

        else
        {
            //float percentage = 0;
            lerpedColor = Color.red;
            GetComponent<Renderer>().material.SetColor("_BaseColor", lerpedColor);
        }
    }
}
