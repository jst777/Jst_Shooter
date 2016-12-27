using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretScript : MonoBehaviour {
	private HealthBar healthBar;
	public GameObject explosion;
	public GameObject pylonObj;

	// Use this for initialization
	void Start () {
		healthBar = GetComponent<HealthBar> ();
		if (healthBar != null) {
			//healthBar.maxHealth = healthBar.currHealth = 10;
		}
	}
	
	// Update is called once per frame
	void Update () {
		CalculateTurretAngle ();	
	}

	void CalculateTurretAngle()
	{
		GameObject player = GameObject.FindWithTag ("Player");
		if (player != null) {
			//float angle = Vector3.Angle (player.transform.position, this.transform.position);
			//pylonObj.transform.rotation.SetLookRotation (player.transform.position);

			Transform childTransform = this.gameObject.transform.FindChild ("Base");
			if (childTransform != null) {
				Transform turretTransform = childTransform.FindChild ("Pylon");
				if (turretTransform != null) {
					
					GameObject turretObj = turretTransform.gameObject; //this.gameobject -> rotate all (X)
					//Debug.Log ("turretObj " + turretObj.ToString ());
					if (turretObj != null) {
						Vector3 vDir = player.transform.position - turretObj.transform.position;
						vDir.y = 0;
						//turretObj.transform.rotation.SetLookRotation (vDir);
						float angle = Vector3.Angle (vDir.normalized, turretObj.transform.forward);
						//Debug.Log ("turretObj.transform.forward = " + turretObj.transform.forward.ToString());

						//TodO: not parent. only pylon rotate toto 2016.12.26

						//turretObj.transform.Rotate (Vector3.forward, angle);
						//turretObj.transform.localRotation.Set (0, 0, angle, 1);
						Quaternion tmpBaseRot = turretObj.transform.localRotation;
						Quaternion targetRotationTurret = Quaternion.LookRotation (vDir, turretObj.transform.parent.up); //get a rotation for the turret that looks toward the target
						turretObj.transform.rotation = Quaternion.Slerp(turretObj.transform.rotation, targetRotationTurret, Time.deltaTime * 10); //gradually turn towards the target at the specified turnSpeed
						//turretObj.transform.localRotation = Quaternion.Euler( turretObj.transform.localRotation.eulerAngles.x, tmpBaseRot.eulerAngles.y, tmpBaseRot.eulerAngles.x); //reset x and z rotations and only rotates the y on its local axis
						turretObj.transform.localRotation = Quaternion.Euler( tmpBaseRot.eulerAngles.x, tmpBaseRot.eulerAngles.y, turretObj.transform.localRotation.eulerAngles.z);
					}
				}
			}

		}
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log ("EnemyTurretScript - OnTriggerEnter" + other.gameObject.name);
		if (other.gameObject.tag == "Bullet") {

			//Destroy (other.gameObject);

			BulletCtrl bulletCtrl = other.gameObject.GetComponent<BulletCtrl>();
			//내부에서 지운다
			bulletCtrl.CollisionDetected ();
			//if (GetComponent<EnemyState> ().enemyState == EnemyState.eEnemyState.eNormalState) {
				OnDamaged ();

			//}
		}
	}

	public void OnDamaged()
	{
		//if (GetComponent<EnemyState> ().enemyState == EnemyState.eEnemyState.eImmortalState)
		//	return;

		if (healthBar != null) {
			if (healthBar.currHealth > 0) {
				healthBar.currHealth -= 1;
				if (healthBar.currHealth <= 0) {
					Instantiate(explosion, transform.position, transform.rotation);
					Destroy (this);
				}
			}
		}

	}
}
