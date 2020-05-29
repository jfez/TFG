using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public PickUp pickUp;
    public TakePhoto takePhoto;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") && collider.gameObject.GetComponent<TakePhoto>().keyPicked)
        {
            if (pickUp != null && takePhoto != null)
            {
                Debug.Log("OPEN DOOR");
                GameManager.Instance.onPositionToDisolve = true;
                pickUp.DestroyCamera();
                takePhoto.DestroyPhoto();
                Destroy(gameObject);
            }
            
        }
    }
}
