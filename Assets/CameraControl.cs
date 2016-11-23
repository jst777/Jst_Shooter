using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	public GameObject player;       //Public variable to store a reference to the player game object
	private Vector3 offset;         //Private variable to store the offset distance between the player and camera
	private bool endingCameraAction;

	// Use this for initialization
	void Start () {
		//offset.Set (0, 30, 15);
		offset.Set (0, 40, 15);
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) {
		}

		endingCameraAction = false;

		UpdateChasingPlayer ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//UpdateChasingPlayer ();
		if (endingCameraAction) {
			if (player != null) {
				GameObject endingCamPos = GameObject.Find ("EndingCamPos");
				transform.position = Vector3.Slerp (transform.position, endingCamPos.transform.position, 10 * Time.deltaTime);
				transform.LookAt(player.transform);
			}
		}
	}
	void LateUpdate () 
	{
		// Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
		//transform.position = player.transform.position + offset;
	}

	public void SetEndingCameraAction()
	{
		endingCameraAction = true;
	}

	void UpdateChasingPlayer(){
		// Create a postion the camera is aiming for based on the offset from the target.
		Vector3 targetCamPos = player.transform.position + offset;

		// Smoothly interpolate between the camera's current position and it's target position.
		//transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
		transform.position = targetCamPos;
		//Vector3 lookDir = player.transform.position - transform.position;
		Vector3 lookDir =targetCamPos;

		lookDir.Set (0, -1, 0);
		transform.rotation = Quaternion.LookRotation (lookDir.normalized);
	}
}