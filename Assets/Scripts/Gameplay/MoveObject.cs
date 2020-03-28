using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    
    private Transform guide;
    private float grabDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        
        guide = GameObject.FindGameObjectWithTag("Player").transform;
        grabDistance = 30f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown(){
        
        if (Vector3.Distance(guide.position, transform.position) < grabDistance)
        {
            transform.parent = guide.transform.transform;
        }
        
        
        
    }

    void OnMouseUp(){
        
        transform.parent = null;
        
        
        
    }
}
