using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour {
	public GameObject bullet;
	public Transform firePos;
	// Use this for initialization
	public float fireTerm = 0.3f;

	void Start () {
		StartCoroutine (FireCoroutine());
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator FireCoroutine()
	{
		while (true) {
			yield return new WaitForSeconds (fireTerm);

			Fire ();
		}
	}

	public void Fire()
	{
		CreateBullet ();
	}
	void CreateBullet()
	{
		GameObject bulletObj = Instantiate (bullet, firePos.position, firePos.rotation);
		bulletObj.GetComponent<BulletCtrl> ().AddForce = true;
	}
}
