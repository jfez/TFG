﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePuzzle : MonoBehaviour
{
    private Transform guide;
    

    private Vector3 mOffset;
    private float mZCoord;
    private Rigidbody rb;

    public GameObject colliderPortal;
    public GameObject colliderStop;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = true;
        guide = GameObject.FindGameObjectWithTag("guide").transform;
        colliderPortal.SetActive(false);
        colliderStop.SetActive(true);
        
        
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
        

        rb.isKinematic = true;
        rb.useGravity = false;

        transform.parent = guide;

        if (!colliderPortal.activeSelf)
        {
            colliderPortal.SetActive(true);
            colliderStop.SetActive(false);
        }
        
        
        
    }

    void OnMouseUp()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        transform.parent = null;
    }

    void OnMouseDrag()
    {
        if (!ManagerMenu.Instance.paused)
        {
            transform.position = GetMouseAsWorldPoint() + mOffset;
        }
        
    }
}
