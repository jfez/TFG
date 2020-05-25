using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchManager : MonoBehaviour
{
    public AudioClip timeClip;
    public AudioClip voxophoneClip;
    
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScratchTime()
    {
        audioSource.spatialBlend = 0f;
        audioSource.clip = timeClip;
        audioSource.Play();
    }

    public void ScratchVoxophone()
    {
        audioSource.spatialBlend = 1f;
        audioSource.clip = voxophoneClip;
        audioSource.Play();
    }
}
