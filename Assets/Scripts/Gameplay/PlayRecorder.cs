using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRecorder : MonoBehaviour
{
    public Transform Eyes;
    public AudioClip dialogueClip;
    

    //private AudioSource recorder;
    private float playDistance;

    public float timeOffset;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //recorder = GetComponent<AudioSource>();
        playDistance = 5f;
    }

    // Update is called once per frame
    void Update()
    {
       
                
    }

    void OnMouseDown()
    {
        //If we want to limitate the distance to play the recorder
        if (Vector3.Distance(Eyes.position, transform.position) < playDistance && !ManagerMenu.Instance.paused)
        {
            //We play the tape if there is not any tape playing 
            //What if we change the tape??
            if (DialogueManager.Instance.audioSource.clip == null)    // || DialogueManager.Instance.audioSource.clip != dialogueClip
            {
                DialogueManager.Instance.BeginDialogue(dialogueClip, timeOffset);
            }
            
        }
    }
}
