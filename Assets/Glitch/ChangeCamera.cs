using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;

    private float timer;
    private bool inside;

    private AudioSource audioShock;

    private bool tp;

    //private Rigidbody rb;
    private CharacterController characterController;
    
    // Start is called before the first frame update
    void Start()
    {
        //Llunota hambrienta recuerdalo cuando estes programando estas cosas raras
        //recuerda que te ama con locura!
        timer = 0f;
        inside = false;
        camera1.enabled = true;
        camera2.enabled = false;

        audioShock = GetComponent<AudioSource>();
        tp = false;
        //rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>() ;
    }

    // Update is called once per frame
    void Update()
    {
        if (inside)
        {
            timer += Time.deltaTime;
        }

        if (timer >= 0.2f && !tp)
        {
            characterController.enabled = false;
            characterController.transform.position = new Vector3(characterController.transform.position.x, characterController.transform.position.y, -4);
            characterController.enabled = true;
            tp = true;
        }

        if (timer >= 0.9f)
        {
            camera1.enabled = true;
            camera2.enabled = false;
            inside = false;
            timer = 0f;
            tp = false;
            //rb.isKinematic = true;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            camera1.enabled = false;
            camera2.enabled = true;
            inside = true;
            audioShock.Play();
            //rb.isKinematic = false;
            //rb.AddForce(-transform.forward * 1000, ForceMode.Impulse);
            
                
            //change first person controller position

            /*characterController.enabled = false;
            characterController.transform.position = new Vector3(-19, 1.6f, 0);
            characterController.enabled = true;*/   



        }
    }

}
