using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {

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


			HealthBar healthBar = GetComponent<HealthBar> ();
			if (healthBar != null) {
				healthBar.currHealth -= 1;

				if (healthBar.currHealth <= 0) {
					//unactivated object can't found in gameobject.find
					//so i found that in uimgr
					GameObject uiMgrobj = GameObject.Find ("UIManager");
					if (uiMgrobj != null) {
						UIMgr uiMgr = uiMgrobj.GetComponentInChildren<UIMgr> ();
						if (uiMgr != null) {
							uiMgr.gameOver.SetActive (true);
						}
					}

					this.gameObject.SetActive(false);
				}
			}
		}
	}
}
