﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour {
	public enum eFireType{
		eNormalFireType,
		eCircleType,
		eNWayFireType,
		eSegmentType,
		eMaxFireType
	}


	public float fireTerm = 2.0f;
	public GameObject bullet;
	public Transform firePos;

	public eFireType fireType;

	// Use this for initialization
	void Start () {
		
		fireType = eFireType.eCircleType;
		{
			if (Camera.main != null) {
				GameMgr gamemgr = Camera.main.GetComponent<GameMgr> ();
				if (gamemgr != null) {
					int fireTypeInt = (gamemgr.currentStage - 1) % (int)eFireType.eMaxFireType;
					if (tag == "Turret") {
						fireTypeInt = (gamemgr.currentStage) / (int)eFireType.eMaxFireType -1;
						if (fireTypeInt >= (int)eFireType.eMaxFireType)
							fireTypeInt = (int)eFireType.eSegmentType;
						
					}
					fireType = (eFireType)fireTypeInt;

				}
			}
		}
		if(tag != "BOSS"){
			if (GetComponent<EnemyFormation> () != null) {
				if (GetComponent<EnemyFormation> ().formation == EnemyFormation.eFormation.eCarrierFormation) {
					fireType = eFireType.eNormalFireType;
					return;
				}
			}
		}
		StartCoroutine (FireCoroutine());

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator FireCoroutine()
	{
		while (true) {
			yield return new WaitForSeconds (fireTerm);

			EnemyState enemyStateComponent = GetComponent<EnemyState> ();
			if (enemyStateComponent != null) {
				if(enemyStateComponent.enemyState != EnemyState.eEnemyState.eImmortalState)
					Fire ();
			}

		}
	}

	public void Fire()
	{
		CreateBullet ();
	}
	void CreateBullet()
	{
		Debug.Log ("EnemyFire");
		GameObject player = GameObject.Find ("Player");
		if (player == null || !player.activeSelf)
			return;
		if (fireType == eFireType.eNormalFireType) {
			GameObject bulletObj = Instantiate (bullet, firePos.position, firePos.rotation);
			bulletObj.GetComponent<BulletCtrl> ().AddForce = true;
			Vector3 vDir = player.transform.position - firePos.position;
			bulletObj.transform.forward = vDir.normalized;
		} else if (fireType == eFireType.eNWayFireType) {
			float theta = 10;//5;
			int bulletCount = 7;//10;
			FireNWayType (theta, bulletCount, firePos);
		} else if (fireType == eFireType.eSegmentType) {
			GameObject bulletObj = Instantiate (bullet, firePos.position, firePos.rotation);
			bulletObj.GetComponent<BulletCtrl> ().AddForce = true;

			bulletObj.GetComponent<BulletCtrl> ().parentObj = gameObject;
			bulletObj.GetComponent<BulletCtrl> ().segMentFlag = true;
			bulletObj.GetComponent<BulletCtrl> ().segmentDistanceFromParent = 7;

			Vector3 vDir = player.transform.position - firePos.position;
			bulletObj.transform.forward = vDir.normalized;
		} else if (fireType == eFireType.eCircleType) {
			int bulletCount = 10;
			FireCircleType (bulletCount, firePos);
		}
	}

	public void FireNWayType(float theta, int bulletCount, Transform fireTransform)//, Vector3 fireForward)
	{
		float rad_step = Mathf.PI / 180 * theta;

		float rad = 0;
		if (bulletCount % 2 == 1)
			rad = -bulletCount / 2 * rad_step;
		else
			rad = (-bulletCount / 2 + 0.5f) * rad_step;

		for (int i = 0; i < bulletCount; i++) {
			float c = Mathf.Cos(rad);
			float s = Mathf.Sin (rad);

			GameObject bulletObj = Instantiate (bullet, fireTransform.position, fireTransform.rotation);
			GameObject player = GameObject.Find ("Player");
			Vector3 vDir = transform.forward;
			if (player != null) {
				vDir = player.transform.position - firePos.position;
			}
			Vector3 velocity = vDir.normalized;//transform.forward;
			velocity.x = velocity.x * c - velocity.z * s;
			velocity.z = velocity.x * s + velocity.z * c;
			//velocity.y = fireTransform.position.y;


			bulletObj.transform.forward = velocity;
			bulletObj.GetComponent<BulletCtrl> ().AddForce = true;
			rad += rad_step;

			//Debug.Log ("index" + i.ToString ()+ "Velocity=" + velocity.ToString () + "rad=" + rad.ToString ());
		}
	}

	public void FireCircleType(int bulletCount, Transform fireTransform)
	{
		
		float rad_step = Mathf.PI * 2 / bulletCount;
		float rad = bulletCount % 2 == 1 ? rad_step / 2 : 0;

		for (int i = 0; i < bulletCount; i++, rad += rad_step) {
			GameObject bulletObj = Instantiate (bullet, firePos.position, fireTransform.rotation);

			GameObject player = GameObject.Find ("Player");
			Vector3 vDir = transform.forward;
			if (player != null) {
				vDir = player.transform.position - firePos.position;
			}
			Vector3 velocity = vDir.normalized;//transform.forward;

			velocity.x = Mathf.Cos (rad);
			velocity.z = Mathf.Sin (rad);
			//velocity.y = fireTransform.position.y;


			bulletObj.transform.forward = velocity;
			bulletObj.GetComponent<BulletCtrl> ().AddForce = true;
		}
	}



}
