using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoKey : MonoBehaviour
{
    public TakePhoto takePhoto;
    
    private Transform Eyes;
    private float photoDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        Eyes = GameObject.FindGameObjectWithTag("MainCamera").transform;
        photoDistance = 30f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        //If we want to limitate the distance to play the recorder
        if (Vector3.Distance(Eyes.position, transform.position) < photoDistance && !ManagerMenu.Instance.paused && takePhoto.focus)
        {
            StartCoroutine(KeyPicked());
        }
    }

    private IEnumerator KeyPicked()
    {
        yield return new WaitForSeconds(1f);
        takePhoto.keyPicked = true;
        Debug.Log("TENEMOS LA LLAVE");
    }
}
