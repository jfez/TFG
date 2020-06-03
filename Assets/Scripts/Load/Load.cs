using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Load : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        //Start async operation
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation()
    {
        //create an async operation
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync("Game");
        
        while (gameLevel.progress < 1)
        {
            yield return new WaitForEndOfFrame();
        }
    }
}
