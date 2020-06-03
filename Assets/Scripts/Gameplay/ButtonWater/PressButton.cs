using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButton : MonoBehaviour
{
    public Transform particlesSpawn;
    public GameObject particlesSystem;
    public PressButton other;
    [HideInInspector]
    public bool activated;
    private AudioSource scratchAudioSource;
    private float timeOffset;

    public AudioClip dialogueClip;
    public AudioSource rainAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        scratchAudioSource = GameObject.FindGameObjectWithTag("Scratch").GetComponent<AudioSource>();
        timeOffset = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (!activated && !other.activated)
        {
            activated = true;
            GameManager.Instance.decision = true;
            
            //Instantiate(particlesSystem, particlesSpawn.position, Quaternion.identity);
            //other disolve
            //este se pone bonito

            PlayAudio();
        }

        GameObject particles = Instantiate(particlesSystem, particlesSpawn.position, Quaternion.identity); 
        Destroy(particles, 7f);
    }

    public void NoDecision()
    {
        Debug.Log("NO DECISION");
    }

    public void PlayAudio()
    {
        if (DialogueManager.Instance.audioKindEnum == AudioKind.AudioKindEnum.Time)
        {
            scratchAudioSource.gameObject.GetComponent<ScratchManager>().ScratchTime();
        }

        else if (DialogueManager.Instance.audioKindEnum == AudioKind.AudioKindEnum.Voxophone)
        {
            scratchAudioSource.gameObject.GetComponent<ScratchManager>().ScratchVoxophone();
        }

        DialogueManager.Instance.audioSource.spatialBlend = 0f;
        DialogueManager.Instance.BeginDialogue(dialogueClip, timeOffset, AudioKind.AudioKindEnum.Event);

        rainAudio.Play();
    }
}
