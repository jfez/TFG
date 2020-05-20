using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class realTime : MonoBehaviour
{
    public Text clockText;
    [HideInInspector]
    public int hour;
    [HideInInspector]
    public int minute;
    [HideInInspector]
    public bool running;
    [HideInInspector]
    public bool onTime;

    public System.DateTime updatedTime;

    private GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        running = true;
        onTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (running)
        {
            System.DateTime time = System.DateTime.Now;

            if (hour != System.DateTime.Now.Hour)
            {
                hour = System.DateTime.Now.Hour;
            }

            if (minute != System.DateTime.Now.Minute)
            {
                minute = System.DateTime.Now.Minute;
            }

            clockText.text = time.ToString("HH:mm");
        }
        
        
        else
        {
            if (!onTime)
            {
                if (updatedTime.Hour == System.DateTime.Now.Hour &&
                updatedTime.Minute == System.DateTime.Now.Minute)
                {
                    if (!gameManager.colliderThirdPortal.activeSelf)
                    {
                        gameManager.OpenThirdPortal();
                    }
                }

                else
                {
                    if (gameManager.colliderThirdPortal.activeSelf)
                    {
                        gameManager.CloseThirdPortal();
                    }
                }
            }

            else
            {
                if (!gameManager.colliderThirdPortal.activeSelf)
                {
                    gameManager.OpenThirdPortal();
                }
            }
            
        }
    }
}
