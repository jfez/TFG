using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuillotineManager : MonoBehaviour
{
    public GameObject head;
    
    private Rigidbody guillotineRB;
    private Rigidbody headRB;
    private float forceExecution;
    
    // Start is called before the first frame update
    void Start()
    {
        guillotineRB = GetComponent<Rigidbody>();
        guillotineRB.useGravity = false;
        guillotineRB.isKinematic = true;

        headRB = head.GetComponent<Rigidbody>();
        headRB.useGravity = false;
        headRB.isKinematic = true;

        forceExecution = 250f;
    }

    public void Execution()
    {
        guillotineRB.isKinematic = false;
        guillotineRB.useGravity = true;
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == head)
        {
            headRB.isKinematic = false;
            headRB.useGravity = true;
            headRB.AddForce(-head.transform.up * forceExecution);
        }
    }
}
