using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    //public Transform player;
    public CharacterController player;
	public Transform reciever;

	private bool playerIsOverlapping = false;
	private AudioSource audiotp;
	private CamerasPortalsManagement camerasPortalsManagement;

	//public Transform ownGFX;
	//public Transform otherGFX;

	void Start()
	{
		audiotp = GetComponent<AudioSource>();
		camerasPortalsManagement = GameObject.FindGameObjectWithTag("CamPortalsManagement").GetComponent<CamerasPortalsManagement>();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerIsOverlapping)
		{
			Vector3 portalToPlayer = player.transform.position - transform.position;
			float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

			// If this is true: The player has moved across the portal
			if (dotProduct < 0f)
			{
				camerasPortalsManagement.NextIsland();
				
				// Teleport him!
				float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
				rotationDiff += 180;
				player.transform.Rotate(Vector3.up, rotationDiff);

				Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
				player.enabled = false;
                player.transform.position = reciever.position + positionOffset;
                player.enabled = true;

				//ownGFX.Rotate (0,0,180);
				//otherGFX.Rotate (0,0,180);

				playerIsOverlapping = false;
			}
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
		{
			playerIsOverlapping = true;
			audiotp.Play();
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player")
		{
			playerIsOverlapping = false;
		}
	}
}
