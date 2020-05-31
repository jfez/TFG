using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoopManagerGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BreakLoop()
    {
        PlayerPrefs.SetInt("Loop", 1);
        Debug.Log("LOOP BROKEN");
    }
}
