using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePuzzle : MonoBehaviour
{
    private Transform guide;
    

    private Vector3 mOffset;
    private float mZCoord;
    private Rigidbody rb;

    public GameObject colliderPortal;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = true;
        guide = GameObject.FindGameObjectWithTag("guide").transform;
        colliderPortal.SetActive(false);
        
        
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
        rb.isKinematic = true;
        rb.useGravity = false;
        
        //transform.position = guide.transform.position;
        //transform.rotation = guide.transform.rotation;
        transform.parent = guide;

        if (!colliderPortal.activeSelf)
        {
            colliderPortal.SetActive(true);
        }
        
        
        
    }

    void OnMouseUp(){
        //item.GetComponent<Rigidbody>().useGravity = true;
        //rb.isKinematic = true;
        rb.isKinematic = false;
        rb.useGravity = true;
        transform.parent = null;
        //transform.position = guide.position;
        
        
        
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseAsWorldPoint() + mOffset;
    }
}
