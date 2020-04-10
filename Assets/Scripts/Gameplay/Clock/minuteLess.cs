using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class minuteLess : MonoBehaviour
{
    public Text clockText;
    
    private Transform Eyes;
    private float changeDistance;
    private realTime realTime;
    
    // Start is called before the first frame update
    void Start()
    {
        Eyes = GameObject.FindGameObjectWithTag("MainCamera").transform;
        realTime = GameObject.FindGameObjectWithTag("RealTime").GetComponent<realTime>();
        changeDistance = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        //If we want to limitate the distance to play the recorder
        if (Vector3.Distance(Eyes.position, transform.position) < changeDistance && !ManagerMenu.Instance.paused && !realTime.running)
        {
            if (realTime.minute > 0)
            {
                realTime.minute--;
            }

            else
            {
                realTime.minute = 59;
            }
            

            System.DateTime time = new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day, realTime.hour, realTime.minute, System.DateTime.Now.Second);

            clockText.text = time.ToString("HH:mm");

            realTime.updatedTime = time;
            
        }
    }
}
