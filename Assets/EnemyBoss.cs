using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour {
	private GameObject leftTurret;
	private GameObject rightTurret;
	public static GameObject turretPrefab = null;

	// Use this for initialization
	void Start () {
		//turretPrefab = (GameObject)Resources.Load ("Prefabs/Enemy_Turret");
		//Debug.Log ("turretPrefab = " + turretPrefab.ToString ());
		if (turretPrefab != null) {
			/*
			leftTurret = Instantiate (turretPrefab, transform.position, transform.rotation);
			leftTurret.transform.parent = this.transform;
			leftTurret.pa
			leftTurret.transform.position  = new Vector3 (1.62f, 0.812f, 1.41f);


			rightTurret = Instantiate (turretPrefab,transform.position, transform.rotation);
			rightTurret.transform.parent = this.transform;
			rightTurret.transform.position  = new Vector3 (-1.62f, 0.812f, 1.41f);
			*/
		}

		//leftTurret.transform.position = new Vector3 (1.62f, 0.812f, 1.41f);
		//rightTurret.transform.position = new Vector3 (-1.62f, 0.812f, 1.41f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
