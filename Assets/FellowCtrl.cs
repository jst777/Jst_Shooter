using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FellowCtrl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);

		MeshRenderer meshRender = GetComponentInChildren<MeshRenderer> ();
		if (meshRender != null) {
			if (meshRender.materials.Length > 0) {
				Debug.Log ("Boss - Material set");
				//meshRender.material.color = Color.red;
				meshRender.materials [0].color = Color.blue;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
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
				}
			}
		}
	}
}
