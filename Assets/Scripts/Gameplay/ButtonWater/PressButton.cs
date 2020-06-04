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
    public GameObject[] objectsToDissolve;
    
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
            rainAudio.Play();
            
            GameObject particles = Instantiate(particlesSystem, particlesSpawn.position, Quaternion.identity); 
            Destroy(particles, 7f);
            foreach(GameObject mesh in objectsToDissolve)
            {
                StartCoroutine(DissolveStatue(mesh));
            }
            

            PlayAudio();
        }

        
    }

    private IEnumerator DissolveStatue(GameObject mesh)
    {
        float timeDissolve = 3f;
        float lerpValue = 0;
        
        
        // loop over 3 seconds backwards
        for (float i = 0; i <= timeDissolve; i += Time.deltaTime)
        {
            lerpValue = i / timeDissolve;
            mesh.GetComponent<MeshRenderer>().material.SetFloat("_Mask", lerpValue);
            yield return null;
        }

        Destroy(mesh);
    }

    public void NoDecision()
    {
        if (!activated && !other.activated)
        {
            activated = true;
            GameManager.Instance.decision = true;
            
            
            foreach(GameObject mesh in objectsToDissolve)
            {
                StartCoroutine(DissolveStatue(mesh));
            }

            foreach (GameObject mesh in other.objectsToDissolve)
            {
                StartCoroutine(DissolveStatue(mesh));
            }
        }
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

        
    }
}
