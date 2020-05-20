using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int indexIsland;

    public AudioSource audioScreams;
    public GameObject colliderSecondPortal;
    public GameObject colliderSecondStop;

    public GameObject colliderThirdPortal;
    public GameObject colliderThirdStop;
    public GlobalTimeManager globalTimeManager;
    public GameObject physicalSun;
    public realTime realTimeManager;
    
    // Start is called before the first frame update
    void Start()
    {
        indexIsland = 1;
        physicalSun.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateScreams()
    {
        audioScreams.Play();
    }

    public void OpenSecondPortal()
    {
        if (!colliderSecondPortal.activeSelf)
        {
            colliderSecondPortal.SetActive(true);
            colliderSecondStop.SetActive(false);
        }
    }

    public void OpenThirdPortal()
    {

        colliderThirdPortal.SetActive(true);
        colliderThirdStop.SetActive(false);
        
    }

    public void CloseThirdPortal()
    {
        colliderThirdPortal.SetActive(false);
        colliderThirdStop.SetActive(true);
        
    }

    public void ActivatePhysicSun()
    {
        physicalSun.SetActive(true);
    }
    public void DeactivatePhysicSun()
    {
        realTimeManager.running = false;
        physicalSun.SetActive(false);
    }

    public bool GetIsNight()
    {
        return globalTimeManager.isNight;
    }
}
