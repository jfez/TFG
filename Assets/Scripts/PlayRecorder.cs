using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRecorder : MonoBehaviour
{
    public Transform Eyes;
    

    private AudioSource recorder;
    private float playDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        recorder = GetComponent<AudioSource>();
        playDistance = 5f;
    }

    // Update is called once per frame
    void Update()
    {
       
                
    }

    void OnMouseDown()
    {
        //If we want to limitate the distance to play the recorder
        if (Vector3.Distance(Eyes.position, transform.position) < playDistance)
        {
            if (recorder.isPlaying)
            {
                recorder.Pause();
            }

            else
            {
                recorder.Play();
            }
            
            
            
        }
    }
}
