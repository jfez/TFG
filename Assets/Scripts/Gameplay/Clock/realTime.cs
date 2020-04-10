using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class realTime : MonoBehaviour
{
    public Text clockText;
    public int hour;
    public int minute;

    public bool running;

    public System.DateTime updatedTime;
    
    // Start is called before the first frame update
    void Start()
    {
        running = true;
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
            if (updatedTime.Hour == System.DateTime.Now.Hour &&
            updatedTime.Minute == System.DateTime.Now.Minute)
            {
                Debug.Log("Presente --> Portal abierto");
            }
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            running = false;
        }
    }
}
