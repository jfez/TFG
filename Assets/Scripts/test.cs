using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject item;
    public GameObject tempParent;
    public Transform guide;
    public bool carry;

    private Vector3 mOffset;
    private float mZCoord;

    
    
    // Start is called before the first frame update
    void Start()
    {
        item.GetComponent<Rigidbody>().useGravity = true;
        carry = false;
        
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
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
        
        item.GetComponent<Rigidbody>().useGravity = false;
        item.GetComponent<Rigidbody>().isKinematic = true;
        //item.transform.position = guide.transform.position;
        //item.transform.rotation = guide.transform.rotation;
        item.transform.parent = guide.transform.transform;
        if (!carry){
            carry = true;

        }
        
    }

    void OnMouseUp(){
        item.GetComponent<Rigidbody>().useGravity = true;
        item.GetComponent<Rigidbody>().isKinematic = false;
        item.transform.parent = null;
        //item.transform.position = guide.transform.position;
        if (carry){
            carry = false;

        }
        
        
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseAsWorldPoint() + mOffset;
    }
}
