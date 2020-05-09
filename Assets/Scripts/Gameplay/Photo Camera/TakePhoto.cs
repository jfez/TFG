﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TakePhoto : MonoBehaviour
{
    [HideInInspector]
    public bool cameraPicked;

    public GameObject cameraObject;
    public GameObject canvasReticula;
    public GameObject canvasPhoto;
    public GameObject photoPrefab;

    private bool focus;
    
    private string finalPath;
    private Sprite last_screenshot_save;

    private Transform guide;

    private GameObject photoObject;

    private Animator cameraAnimator;

    // Start is called before the first frame update
    void Start()
    {
        cameraPicked = false;
        focus = false;
        canvasPhoto.SetActive(false);
        guide = GameObject.FindGameObjectWithTag("Player").transform;
        cameraAnimator = cameraObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraPicked && !ManagerMenu.Instance.paused)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("enfoque");
                focus = true;
                cameraAnimator.SetTrigger("Focus");
                //cameraObject.SetActive(false);
                canvasReticula.SetActive(false);
                canvasPhoto.SetActive(true);
                if (photoObject != null)
                {
                    photoObject.SetActive(false);
                }
            }

            if (Input.GetMouseButtonUp(1))
            {
                Debug.Log("desenfoque");
                focus = false;
                cameraAnimator.SetTrigger("Unfocus");
                //cameraObject.SetActive(true);
                canvasReticula.SetActive(true);
                canvasPhoto.SetActive(false);
                if (photoObject != null)
                {
                    photoObject.SetActive(true);
                }
            }

            if (focus && Input.GetMouseButtonDown(0))
            {
                StartCoroutine(TakingPhoto());
            }
        }
    }

    private IEnumerator TakingPhoto()
    {
        Debug.Log("PHOTO");
        string fileName = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");

        finalPath = (Application.persistentDataPath + "/Screenshot " + fileName + ".png");
        
        ScreenCapture.CaptureScreenshot(finalPath);
        yield return new WaitForSeconds(0.1f);
        last_screenshot_save = LoadSprite(finalPath);
        
        
        
        Debug.Log("changing");
        
        if (photoObject == null)
        {
            photoObject = Instantiate(photoPrefab, guide.transform.position, photoPrefab.transform.rotation, guide);
            photoObject.transform.localPosition = new Vector3 (-1,0,1.7f);
            photoObject.transform.localRotation = Quaternion.identity;
            photoObject.SetActive(false);
        }
        
        photoObject.GetComponent<SpriteRenderer>().sprite = last_screenshot_save;
        
        File.Delete(finalPath);
    }

    private Sprite LoadSprite(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return null;
        } 
        if (System.IO.File.Exists(path))
        {
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return sprite;
        }
        
        return null;
    }
}
