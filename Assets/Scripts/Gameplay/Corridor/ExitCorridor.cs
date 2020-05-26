using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCorridor : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            GameManager.Instance.indexAudio = 0;
            GameManager.Instance.finishAudios = false;
            GameManager.Instance.indexInsideIsland++;
        }
    }
}
