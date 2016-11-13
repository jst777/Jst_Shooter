using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour {
	public GameObject bullet;
	public Transform firePos;
	// Use this for initialization
	public float fireTerm = 0.3f;

	private LaserBeam laser = null;
	public bool stopFire {
		get;
		set;

	}

	void Start () {
		StartCoroutine (FireCoroutine());
		 laser = GetComponentInChildren<LaserBeam> ();
		stopFire = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator FireCoroutine()
	{
		while (true) {
			yield return new WaitForSeconds (fireTerm);
			if (stopFire)
				break;
			if (laser != null) {
				if (!laser.IsLaserVisible ()) {
					Fire ();
				}
			}
			else
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

	public void FireLaserBeam()
	{
		
		if (laser != null) {
			laser.FireLaserBeam ();
		}
	}
}
