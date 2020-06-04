using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public PickUp pickUp;
    public TakePhoto takePhoto;
    public GameObject[] cables;
    public Material openMaterial;
    public AudioSource soundDoor;
    public Animator animatorSlidingDoor;
    public GameObject panelPhoto;
    // Start is called before the first frame update
    void Start()
    {
        panelPhoto.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        
        if (pickUp != null && takePhoto != null && takePhoto.keyPicked && !takePhoto.focus)
        {
            Debug.Log("OPEN DOOR");
            foreach (GameObject cable in cables)
            {
                cable.GetComponent<MeshRenderer>().material = openMaterial;
            }

            animatorSlidingDoor.SetTrigger("OpenSlidingDoor");
            
            soundDoor.Play();
            GameManager.Instance.onPositionToDisolve = true;
            pickUp.DestroyCamera();
            takePhoto.DestroyPhoto();
            panelPhoto.SetActive(true);
            panelPhoto.GetComponent<SpriteRenderer>().sprite = takePhoto.last_screenshot_save;
            Destroy(gameObject);
        }
            
        
    }
}
