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
       if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Eyes.position, Eyes.forward, out hit, Mathf.Infinity))
            {
                if (hit.transform.tag == "AudioRecorder")
                {
                    //If we want to limitate the distance to play the recorder
                    if (Vector3.Distance(Eyes.position, transform.position) < playDistance)
                    {
                        recorder.Play();
                        //Hay un problema cuando el jugador está demasiado cerca de la grabadora porque si colisionan ambos colliders, no pilla el .hit
                    }
                    
                    
                }
            }
        }
                
    }
}
