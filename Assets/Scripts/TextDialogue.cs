using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDialogue : MonoBehaviour
{
    public AudioClip dialogueClip;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DialogueManager.Instance.BeginDialogue(dialogueClip);
        }
    }
}
