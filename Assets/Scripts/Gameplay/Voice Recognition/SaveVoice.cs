using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveVoice : MonoBehaviour
{
    public AudioClip audioClip;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            if (audioClip != null)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
            }
        }*/
    }

    public void PlayPlayerVoice()
    {
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
            
        }
        Debug.Log("PLAYER VOICE");
    }

    public void StopPlayerVoice()
    {
        audioSource.Stop();
    }
}
