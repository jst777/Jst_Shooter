using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {

	// Use this for initialization
	public GameObject fellowPrefab = null;
	public Transform[] fellowPos;

	void Start () {
		CreateFellow ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CreateFellow()
	{
		//fellowPrefab = (GameObject)Resources.Load("Fellow", typeof(GameObject));
		if (fellowPrefab != null) {
			if (fellowPos.Length > 0) {
				for(int i=0; i< fellowPos.Length; i++)
				{
					GameObject fellow = (GameObject)Instantiate (fellowPrefab, fellowPos [i].position, fellowPos[i].rotation);
					fellow.transform.parent = transform;
					//Debug.Log ("fellowPrefab created");
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

				//device vibrate when hp down
				Handheld.Vibrate ();

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
