using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownJail : MonoBehaviour
{
    private Animator jailAnimator;
    
    void Start ()
    {
        jailAnimator = GetComponent<Animator>();
    }
    
    void OnEnable () 
    {
        VoiceRecognition.ScreamStop += FallDown;
    }

    void OnDisable () 
    {
        VoiceRecognition.ScreamStop -= FallDown;
    }

    void FallDown () 
    {
        jailAnimator.SetTrigger("jail");
    }
}
