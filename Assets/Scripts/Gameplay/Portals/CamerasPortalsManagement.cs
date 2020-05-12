using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasPortalsManagement : MonoBehaviour
{
    public GameObject[] camerasPortals;
    private int indexCamera;
    // Start is called before the first frame update
    void Start()
    {
        indexCamera = 1;
        foreach (GameObject camera in camerasPortals)
        {
            camera.SetActive(false);
        }

        //camerasPortals[0].SetActive(true);
        camerasPortals[1].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextIsland()
    {
        if (indexCamera + 2 > camerasPortals.Length)
        {
            camerasPortals[indexCamera].SetActive(false);
            camerasPortals[indexCamera-1].SetActive(true);
        }

        else
        {
            camerasPortals[indexCamera].SetActive(false);
            camerasPortals[indexCamera-1].SetActive(true);
            camerasPortals[indexCamera+2].SetActive(true);
            indexCamera = indexCamera + 2;
        }

        
    }
}
