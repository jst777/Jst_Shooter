using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour {
	public int damage = 20;
	public float speed = 1000.0f;

	public GameObject parentObj = null;
	public float scaleFactor = 1.0f;

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

		Destroy (gameObject, 5);
	}

	void OnBecameInvisible()
	{
		Destroy (gameObject);
		//Debug.Log ("OnBecameInvisible" + gameObject.name);
	}

	public void CollisionDetected()
	{
		scaleFactor -= 1.0f;
		if (scaleFactor <= 0) {
			Destroy (gameObject);
		}
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
