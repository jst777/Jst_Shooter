using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FellowCtrl : MonoBehaviour {
	public Color color = Color.blue;
	// Use this for initialization
	public int colorIndex = 0;
	Color[] colorList = {Color.blue, Color.red, Color.yellow, Color.green};

	public GameObject shieldPrefab;
	public GameObject shieldObject = null;

	void Start () {
		transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
		/*
		MeshRenderer meshRender = GetComponentInChildren<MeshRenderer> ();
		if (meshRender != null) {
			if (meshRender.materials.Length > 0) {
				
				//meshRender.material.color = Color.red;
				meshRender.materials [0].color = colorList[colorIndex];
			}
		}
		*/
		SetColorIndex (colorIndex);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetColorIndex(int index, float scaleFactor = 1.0f)
	{
		colorIndex = index;

		if (colorIndex < colorList.Length) {
			MeshRenderer meshRender = GetComponentInChildren<MeshRenderer> ();
			if (meshRender != null) {
				if (meshRender.materials.Length > 0) {
					
					//meshRender.material.color = Color.red;
					meshRender.materials [0].color = colorList[colorIndex];
				}
			}
			if (colorList[colorIndex] == Color.yellow) {

				if (shieldPrefab != null) {
					if (shieldObject == null) {
						shieldObject = Instantiate (shieldPrefab, transform.position, transform.rotation);
						shieldObject.transform.localScale = new Vector3 (3 * scaleFactor, 3 * scaleFactor, 3 * scaleFactor);
						//shieldObj.transform.parent = transform;
						shieldObject.transform.parent = transform;
					}
				}
			}
		}
	}


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "EnemyBullet") {
			Destroy (other.gameObject);


			HealthBar healthBar = GetComponent<HealthBar> ();
			if (healthBar != null) {
				healthBar.currHealth -= 1;

				if (healthBar.currHealth <= 0) {
					

					this.gameObject.SetActive (false);
					//get lost
					string fellowName;
					fellowName = "Fellow" + (colorIndex + 1).ToString ();
					PlayerPrefs.SetInt (fellowName, 0);
				}
			}
		}
	}
}
