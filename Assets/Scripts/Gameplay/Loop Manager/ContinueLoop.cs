using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueLoop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (!GameManager.Instance.action && !ManagerMenu.Instance.paused)
        {
            GameManager.Instance.action = true;
            GameManager.Instance.DisableMovement();
            GameManager.Instance.StopScreamsAndPlayer();
            GameManager.Instance.PlayFallDownJail();
            GameManager.Instance.FinalFade();
            
        }
        
    }
}
