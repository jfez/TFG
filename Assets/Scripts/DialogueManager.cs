using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    //Subtitle variables
    private List<string> subtitleLines = new List<string>();

    private List<string> subtitleTimingStrings = new List<string>();
    public List<string> subtitleTimings = new List<string>();

    public List<string> subtitleText = new List<string>();

    private int nextSubtitle = 0;

    private string displaySubtitle;

    //Trigger variables
    private List<string> triggerLines = new List<string>();

    private List<string> triggerTimingStrings = new List<string>();
    public List<float> triggerTimings = new List<float>();

    private List<string> triggers = new List<string>();
    public List<string> triggerObjectNames = new List<string>();
    public List<string> triggerMethodNames = new List<string>();

    private int nextTrigger = 0;


    
    //Singleton property
    public static DialogueManager Instance {get; private set;}

    private AudioSource audioSource;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void BeginDialogue (AudioClip passedClip)
    {
        //Set and play the audioclip
        audioSource.clip = passedClip;
        audioSource.Play();
        
    }
}
