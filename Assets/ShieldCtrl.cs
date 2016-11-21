using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCtrl : MonoBehaviour {
	public int shieldHP = 10;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "EnemyBullet") {
			Destroy (other.gameObject);

			shieldHP -= 1;
		
			if (shieldHP <= 0) {

				FellowCtrl fellowCtrl = GetComponent<FellowCtrl> ();
				if (fellowCtrl != null) {
					if (fellowCtrl.shieldObject != null) {
						DestroyObject (fellowCtrl.shieldObject);
						fellowCtrl.shieldObject = null;
					}
				}
			}
		}
	}
}
