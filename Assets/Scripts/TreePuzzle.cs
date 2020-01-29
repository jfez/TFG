using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePuzzle : MonoBehaviour
{
    private Transform guide;
    

    private Vector3 mOffset;
    private float mZCoord;
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        guide = GameObject.FindGameObjectWithTag("Player").transform;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 GetMouseAsWorldPoint()

    {
        // Pixel coordinates of mouse (x,y)

        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen

        mousePoint.z = mZCoord;

        // Convert it to world points

        return Camera.main.ScreenToWorldPoint(mousePoint);

    }

    void OnMouseDown(){
        
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        // Store offset = gameobject world pos - mouse world pos
        mOffset = transform.position - GetMouseAsWorldPoint();
        
        //item.GetComponent<Rigidbody>().useGravity = false;
        if (rb.isKinematic)
        {
            rb.isKinematic = false;
        }
        
        transform.position = guide.transform.position;
        transform.rotation = guide.transform.rotation;
        transform.parent = guide.transform.transform;
        
        
    }

    void OnMouseUp(){
        //item.GetComponent<Rigidbody>().useGravity = true;
        //rb.isKinematic = true;
        transform.parent = null;
        //item.transform.position = guide.transform.position;
        
        
        
    }

    void OnMouseDrag()
    {
        //transform.position = GetMouseAsWorldPoint() + mOffset;
    }
}
