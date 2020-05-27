using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaze : MonoBehaviour
{
    public int indexCollider;
    public GameObject other;
    public MazeLoader mazeManager;


    // Start is called before the first frame update
    void Start()
    {
        if (indexCollider == 1)
        {
            gameObject.SetActive(false);
        }

        else if (indexCollider == 2)
        {
            gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider collider)
	{
		if (collider.tag == "Player")
		{
            if (indexCollider == 2)
            {
                mazeManager.StartMaze();
                other.gameObject.SetActive(true);
                gameObject.SetActive(false);
                
            }

            else if (indexCollider == 1)
            {
                mazeManager.MakeCorridor();
                gameObject.SetActive(false);
            }
            
            
        }
			
	}
}
