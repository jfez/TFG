using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Load : MonoBehaviour
{
    [SerializeField]
    private Image _progressBar;
    
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
            //take the progress bar fill - async operation progress
            _progressBar.fillAmount = gameLevel.progress;

            yield return new WaitForEndOfFrame();
        }
    }
}
