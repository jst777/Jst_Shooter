using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerShipSwap : MonoBehaviour {
	public int shipIndex = 1;
	// Use this for initialization
	void Start () {
		if (SceneManager.GetActiveScene ().name != "ShopScene") {
			shipIndex = PlayerPrefs.GetInt ("PlayerShip_Index");
		}
		ChangeShipIndex (shipIndex);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeShipIndex(int shipIndex)
	{
		string shipName = "Ship" + shipIndex;
		GameObject prefab = (GameObject)Resources.Load ("Space_Jet/Meshes/" + shipName);
		//if (prefab == null)
		//	Debug.Log ("Prefab = null");
		GetComponentInChildren<MeshFilter> ().mesh = prefab.GetComponent<MeshFilter> ().sharedMesh;

		float scaleFactor = 1.0f;
		scaleFactor *= shipIndex;

		GetComponent<FireCtrl>().scaleFactor = scaleFactor;
	}
}
