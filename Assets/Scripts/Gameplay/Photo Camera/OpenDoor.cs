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
    private float pickUpDistance;

    private Transform Eyes;
    // Start is called before the first frame update
    void Start()
    {
        panelPhoto.SetActive(false);
        Eyes = GameObject.FindGameObjectWithTag("MainCamera").transform;
        pickUpDistance = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        
        if (pickUp != null && takePhoto != null && takePhoto.keyPicked && !takePhoto.focus &&
            Vector3.Distance(Eyes.position, transform.position) < pickUpDistance)
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
