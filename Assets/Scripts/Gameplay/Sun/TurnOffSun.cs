using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffSun : MonoBehaviour
{
    public GlobalTimeManager globalTimeManager;

    private GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (!ManagerMenu.Instance.paused)
        {
            globalTimeManager.SunOff();
            globalTimeManager.isNight = true;
            gameManager.DeactivatePhysicSun();
        }
        
    }
}
