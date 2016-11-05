using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour {
	public int damage = 20;
	public float speed = 1000.0f;

	public GameObject parentObj = null;

	public bool segMentFlag {
		get;
		set;
	}
	public int segmentDistanceFromParent {
		get;
		set;
	}

	public bool AddForce {
		get;
		set;
	}

	void Awake()
	{
		segMentFlag = false;
		segmentDistanceFromParent = 1;

	}

	// Use this for initialization
	void Start () {

		if(AddForce)
			GetComponent<Rigidbody> ().AddForce (transform.forward * speed);	
	}

	void OnBecameInvisible()
	{
		Destroy (gameObject);
		//Debug.Log ("OnBecameInvisible" + gameObject.name);
	}

	
	// Update is called once per frame
	void Update () {
		if (segMentFlag) {
			if (parentObj != null) {
				float fDistance = Vector3.Distance (parentObj.transform.position, transform.position);
				if (fDistance > segmentDistanceFromParent) {

					Debug.Log ("fDistance > segmentDistanceFromParent");
					segMentFlag = false;
					EnemyFire enemyFire = parentObj.GetComponent<EnemyFire> ();
					if (enemyFire != null) {

						Debug.Log ("enemyFire != null");
						enemyFire.FireNWayType (5, 10, gameObject.transform);
					}
				}
			}
		}
	}
}
