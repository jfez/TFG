﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    public CharacterController player;
	public Transform reciever;

	private bool playerIsOverlapping = false;
	private AudioSource audiotp;
	private CamerasPortalsManagement camerasPortalsManagement;

	private GameManager gameManager;


	void Start()
	{
		gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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

			if (GameManager.Instance.indexIsland == 1)
			{
				gameManager.ActivateScreams();
			}

			else if (GameManager.Instance.indexIsland == 2 && !gameManager.GetIsNight())
			{
				gameManager.ActivatePhysicSun();
			}

			GameManager.Instance.indexIsland++;
			GameManager.Instance.indexInsideIsland = 1;
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
