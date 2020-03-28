using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMaze : MonoBehaviour
{
    private Animator animator;

    public delegate void Action();
    public static event Action LockedUp;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            animator.SetTrigger("maze");
            if(LockedUp != null)
                {
                    LockedUp();
                }
            Destroy(gameObject);
        }
    }
}
