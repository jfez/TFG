using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Transform Eyes;
    private Transform guide;
    private float pickUpDistance;

    public TakePhoto takePhoto;
    
    // Start is called before the first frame update
    void Start()
    {
        Eyes = GameObject.FindGameObjectWithTag("MainCamera").transform;
        guide = GameObject.FindGameObjectWithTag("Player").transform;
        pickUpDistance = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        //If we want to limitate the distance to play the recorder
        if (Vector3.Distance(Eyes.position, transform.position) < pickUpDistance && !ManagerMenu.Instance.paused)
        {
            
            transform.parent = guide.transform.transform;
            transform.localPosition = new Vector3 (0.5f,0.2f,1);
            transform.localRotation = Quaternion.identity;
            takePhoto.cameraPicked = true;
            
            
        }
    }
}
