using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackCorridor : MonoBehaviour
{
    public CharacterController player;
    public Transform beginPoint;
    
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
            player.enabled = false;
            player.transform.position = beginPoint.position;
            player.enabled = true;
        }
    }
}
