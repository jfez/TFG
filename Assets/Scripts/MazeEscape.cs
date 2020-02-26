using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeEscape : MonoBehaviour
{
    [HideInInspector]
    public bool locked;

    public GameObject wallBehind;

    // Start is called before the first frame update
    void Start()
    {
        locked = false;
    }

    // Update is called once per frame
    void Update()
    {  
        
    }

    void OnMouseOver()
    {
        if (locked)
        {
            if (Input.GetMouseButton(0))
            {
                wallBehind.SetActive(false);
                
            }

            else
            {
                wallBehind.SetActive(true);
                
            }
        }
    }

    void OnMouseExit()
    {
        wallBehind.SetActive(true);
        
    }

    void OnEnable () 
    {
        TriggerMaze.LockedUp += ActivateLocked;
    }

    void OnDisable () 
    {
        TriggerMaze.LockedUp -= ActivateLocked;
    }

    void ActivateLocked () 
    {
        locked = true;
    }
}
