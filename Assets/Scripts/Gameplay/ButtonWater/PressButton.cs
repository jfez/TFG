using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButton : MonoBehaviour
{
    public Transform particlesSpawn;
    public GameObject particlesSystem;
    public PressButton other;
    [HideInInspector]
    public bool activated;
    
    // Start is called before the first frame update
    void Start()
    {
        activated = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        /*if (!activated && !other.activated)
        {
            Instantiate(particlesSystem, particlesSpawn.position, Quaternion.identity);
            //other disolve
            //este se pone bonito
            //trigger animación vaciar tanque de agua
        }*/

        Instantiate(particlesSystem, particlesSpawn.position, Quaternion.identity);
        
    }
}
