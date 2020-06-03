using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{
    public Camera cameraA;
	public Camera cameraB;
	public Camera cameraC;
	public Camera cameraD;
	public Camera cameraE;
	public Camera cameraF;
	public Camera cameraG;
	public Camera cameraH;


	public Material cameraMatA;
	public Material cameraMatB;
	public Material cameraMatC;
	public Material cameraMatD;
	public Material cameraMatE;
	public Material cameraMatF;
	public Material cameraMatG;
	public Material cameraMatH;



	// Use this for initialization
	void Start () {
		
        if (cameraA.targetTexture != null)
		{
			cameraA.targetTexture.Release();
		}
		cameraA.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
		cameraMatA.mainTexture = cameraA.targetTexture;
        

		if (cameraB.targetTexture != null)
		{
			cameraB.targetTexture.Release();
		}
		cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
		cameraMatB.mainTexture = cameraB.targetTexture;

		if (cameraC.targetTexture != null)
		{
			cameraC.targetTexture.Release();
		}
		cameraC.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
		cameraMatC.mainTexture = cameraC.targetTexture;

		if (cameraD.targetTexture != null)
		{
			cameraD.targetTexture.Release();
		}
		cameraD.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
		cameraMatD.mainTexture = cameraD.targetTexture;
		
		if (cameraE.targetTexture != null)
		{
			cameraE.targetTexture.Release();
		}
		cameraE.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
		cameraMatE.mainTexture = cameraE.targetTexture;

		if (cameraF.targetTexture != null)
		{
			cameraF.targetTexture.Release();
		}
		cameraF.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
		cameraMatF.mainTexture = cameraF.targetTexture;

		if (cameraG.targetTexture != null)
		{
			cameraG.targetTexture.Release();
		}
		cameraG.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
		cameraMatG.mainTexture = cameraG.targetTexture;

		if (cameraH.targetTexture != null)
		{
			cameraH.targetTexture.Release();
		}
		cameraH.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
		cameraMatH.mainTexture = cameraH.targetTexture;

		
	}
}
