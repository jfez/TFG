using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRecorder : MonoBehaviour
{
    private Transform Eyes;
    public AudioClip dialogueClip;
    

    //private AudioSource recorder;
    private float playDistance;

    public float timeOffset;

    private AudioSource scratchAudioSource;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //recorder = GetComponent<AudioSource>();
        scratchAudioSource = GameObject.FindGameObjectWithTag("Scratch").GetComponent<AudioSource>();
        Eyes = GameObject.FindGameObjectWithTag("MainCamera").transform;
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
            
            if (DialogueManager.Instance.audioSource.clip != null && 
            (DialogueManager.Instance.audioKindEnum == AudioKind.AudioKindEnum.Time || DialogueManager.Instance.audioKindEnum == AudioKind.AudioKindEnum.Event))
            {
                return;
            }
            
            else if (DialogueManager.Instance.audioSource.clip == null || DialogueManager.Instance.audioSource.clip != dialogueClip)
            {
                DialogueManager.Instance.transform.position = transform.position;
                scratchAudioSource.gameObject.transform.position = transform.position;
                DialogueManager.Instance.audioSource.spatialBlend = 1f;
                DialogueManager.Instance.BeginDialogue(dialogueClip, timeOffset, AudioKind.AudioKindEnum.Voxophone);
            }

            else if (DialogueManager.Instance.audioSource.clip == dialogueClip)
            {
                DialogueManager.Instance.audioSource.Stop();
                DialogueManager.Instance.audioSource.clip = null;
                DialogueManager.Instance.audioKindEnum = AudioKind.AudioKindEnum.None;
            }
            
        }
    }
}
