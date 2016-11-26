using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
	// Use this for initialization
	public bool isSetPos = false;

	public Vector3 vOffsetToBoss = new Vector3(0,0,0);
	public Vector3 vDestination;

	public Vector3 vInitTargetPos;

	//public float offsetXZ = 1.0f;

	private GameMgr gameMgrRef;

	public GameObject explosion;

    public int destinationIndex {
		get;
		set;
	}
	public int startIndex {
		get;
		set;

	}
	public Transform[] spawnpoints;
	public Transform[] destpoints;

	public float Speed {
		get;
		set;
	}

	public Vector3 Velocity {
		get;
		set;
	}

	private HealthBar healthBar;


	void Start () {
		//startIndex = 2;
		//destinationIndex = 3;
		//Debug.Log("Points Index = " + startIndex.ToString() + "," + destinationIndex.ToString());

		Speed = 1.0f;

		healthBar = GetComponent<HealthBar> ();

		GameObject playerObj = GameObject.Find ("Player");
		if (playerObj != null) {
			vInitTargetPos = playerObj.transform.position;
		}

		gameMgrRef = GameObject.Find ("Camera").GetComponent<GameMgr> ();
		GameObject spawnPoint = GameObject.Find ("SpawnPoint");
		if (spawnPoint != null) {
			spawnpoints = spawnPoint.GetComponentsInChildren<Transform> ();
		}
		GameObject destPoint = GameObject.Find ("DestinationPoint");
		if (destPoint != null) {
			destpoints = destPoint.GetComponentsInChildren<Transform> ();
		}

		//EnemyFormation enemyFormation = GetComponent<EnemyFormation> ();
		//if (enemyFormation) {
		//	enemyFormation.CalculateOffsetPos ();
		//}
		//CalculateOffsetPos ();

		if (tag == "BOSS") {
			//if (healthBar != null) {
			//	healthBar.maxHealth = healthBar.currHealth = 5;
			//}
			Speed = 2.0f;
			transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
		}
		transform.position = spawnpoints[startIndex].position;

		// boss color set
		if (tag == "BOSS") {
			MeshRenderer meshRender = GetComponentInChildren<MeshRenderer> ();
			if (meshRender != null) {
				if (meshRender.materials.Length > 0) {
					Debug.Log ("Boss - Material set");
					//meshRender.material.color = Color.red;
					meshRender.materials [0].color = Color.yellow;
				}
			}
		}
	}

	void Awake(){
		//still not made name so.. call from start
	}

	// Update is called once per frame
	void Update () {
		if (spawnpoints == null || destpoints == null)
			return;
		if (spawnpoints.Length == 0 || destpoints.Length == 0)
			return;
		//if (destpoints [destinationIndex] == null || spawnpoints [startIndex] == null)
		//	return;

		float fDistance = Vector3.Distance(vDestination, transform.position); 

		if (tag == "BOSS") {
			if (fDistance > 1) {
				Vector3 vDir = destpoints[destinationIndex].position - spawnpoints[startIndex].position;
				
				transform.rotation = Quaternion.LookRotation (vDir.normalized);//Quaternion.RotateTowards(transform.rotation, q, rotatieSpeed * Time.deltaTime);
			} else {
				GameObject playerObj = GameObject.Find ("Player");
				if (playerObj != null) {
					Vector3 vDir = vInitTargetPos - transform.position;//playerObj.transform.position - transform.position;

					//transform.rotation = Quaternion.LookRotation(vDir.normalized);

					{
						Quaternion a = new Quaternion ();
						Quaternion b = new Quaternion ();
						a.SetLookRotation (transform.forward);
						b.SetLookRotation (vDir);
						Quaternion.Lerp (a, b, Time.deltaTime);
						transform.rotation = Quaternion.Lerp (a, b, Time.deltaTime);//

						//float angle = Quaternion.Angle (b, playerObj.transform.rotation);
						//Debug.Log ("Boss-Angle " + angle);
					}
				}
				GetComponent<EnemyState> ().enemyState = EnemyState.eEnemyState.eNormalState;
			}
		}
		if (tag == "BOSS") 
		{
			transform.position = Vector3.Slerp (transform.position, destpoints[destinationIndex].position, Speed * Time.deltaTime);

			Velocity = transform.forward;
			Velocity = Velocity.normalized * Speed;
			//transform.position = transform.position + Velocity * Time.deltaTime;
		}
		else {
			GameObject bossObj = GameObject.FindWithTag ("BOSS");
			if (bossObj != null && bossObj.activeSelf) {
				//float fDistanceToBoss = Vector3.Distance (bossObj.transform.position, transform.position);

				if (gameMgrRef != null) {
					EnemyFormation enemyformation = GetComponent<EnemyFormation> ();

					if(enemyformation.formation != EnemyFormation.eFormation.eCarrierFormation)
						OffsetPursuit (bossObj, vOffsetToBoss);
					else {
						float fDistanceToBoss = Vector3.Distance (transform.position, bossObj.transform.position);

						if (enemyformation.carrierAction == EnemyFormation.eCarrierAction.eCarrierSeperation) {
							if (fDistanceToBoss < 5)
								Seperation ();
							else {
								enemyformation.spearationAngleSet = false;
								enemyformation.carrierAction = EnemyFormation.eCarrierAction.eCarrierDashToPlayer;
								GetComponent<EnemyState>().enemyState = EnemyState.eEnemyState.eNormalState;
							}
						}
						else if (enemyformation.carrierAction == EnemyFormation.eCarrierAction.eCarrierDashToPlayer) {
							GameObject player = GameObject.Find ("Player");
							if(player != null){
								
								float fDistanceToPlayer = Vector3.Distance (transform.position, player.transform.position);

								if (fDistanceToPlayer > 6)
									DashToPlayer ();
								else{
									enemyformation.carrierAction = EnemyFormation.eCarrierAction.eCarrierCohesion;
								}
							}
						} 
						else if (enemyformation.carrierAction == EnemyFormation.eCarrierAction.eCarrierCohesion) {
							if(fDistanceToBoss > 2)
								Cohesion ();
							else{
								enemyformation.carrierAction = EnemyFormation.eCarrierAction.eCarrierSeperation;
							}
						}

						/*if(fDistanceToBoss > 10)
						{
							Cohesion ();
						}
						*/
						//else{
						//	Speration();
						//}

						//Speration ();
					}

				}
			} else {
				//boss die set
				if (GetComponent<EnemyState> ().enemyState != EnemyState.eEnemyState.eNormalState)
					GetComponent<EnemyState> ().enemyState = EnemyState.eEnemyState.eNormalState;
				//GameObject playerObj = GameObject.Find ("Player");
				//if (playerObj != null)
				{
					transform.position+= (transform.forward * Speed * Time.deltaTime * 10);

					Debug.Log ("Enemy Move After boss die " + transform.forward.ToString ());
					Plane[] planes = GeometryUtility.CalculateFrustumPlanes (Camera.main);
					//out of sight
					if (!GeometryUtility.TestPlanesAABB (planes, gameObject.GetComponent<Collider> ().bounds)) {
						DisableAndAddScore ();
					}
				}
			}
		}
		//Debug.Log (gameObject.name + " " + transform.position.ToString ());
	}

	void OffsetPursuit(GameObject leader, Vector3 offsetPos){
		if (leader != null) {
			Vector3 worldOffsetPos = new Vector3 (0, 0, 0);
			worldOffsetPos = vOffsetToBoss;
			//transform.LookAt (leader.transform);
			Matrix4x4 mat = new Matrix4x4 ();
			Quaternion quat = Quaternion.LookRotation (-1 * leader.transform.forward);
			mat.SetTRS (leader.transform.position, quat, new Vector3 (1, 1, 1));

			//mat.MultiplyVector(
			//worldOffsetPos = mat.MultiplyVector(worldOffsetPos) + leader.transform.position;
			worldOffsetPos = mat.MultiplyPoint (worldOffsetPos);

			float fDistance = Vector3.Distance (worldOffsetPos, transform.position);

			//arrived
			if (fDistance < 1) {
				GetComponent<EnemyState> ().enemyState = EnemyState.eEnemyState.eNormalState;

				Vector3 vDir = vInitTargetPos - transform.position;
				Quaternion a = new Quaternion ();
				Quaternion b = new Quaternion ();
				a.SetLookRotation (transform.forward);
				b.SetLookRotation (vDir);
				Quaternion.Lerp (a, b, Time.deltaTime);
				transform.rotation = Quaternion.Lerp (a, b, Time.deltaTime);//


				return;
			}

			//Vector3 toOffset = worldOffsetPos - transform.position;
			float lookAheadTime = Vector3.Distance (worldOffsetPos, transform.position) / (Speed + leader.GetComponent<EnemyScript> ().Speed);

			Arrive (worldOffsetPos + leader.GetComponent<EnemyScript> ().Velocity * lookAheadTime, 1);
		} else {
			GetComponent<EnemyState> ().enemyState = EnemyState.eEnemyState.eNormalState;

			Vector3 vDir = vInitTargetPos - transform.position;
			Quaternion a = new Quaternion ();
			Quaternion b = new Quaternion ();
			a.SetLookRotation (transform.forward);
			b.SetLookRotation (vDir);
			Quaternion.Lerp (a, b, Time.deltaTime);
			transform.rotation = Quaternion.Lerp (a, b, Time.deltaTime);//
		}
	}

	void Arrive(Vector3 targetPos, float deceleration)
	{
		Vector3 toTarget = targetPos - transform.position;
		float fDistance = Vector3.Distance (targetPos, transform.position);
		if (fDistance > 0) {
			
			const float declerationtweaker = 0.3f;

			float speed = fDistance / (deceleration * declerationtweaker);

			speed = Mathf.Min (speed, Speed);

			Vector3 desiredVelocity = toTarget * speed / fDistance;

			Vector3 prevPositon = transform.position;
			//transform.position = transform.position + (desiredVelocity - Velocity) *  Time.deltaTime * 10;
			Vector3 newPosition = transform.position + (desiredVelocity - Velocity);

			//twisting because of this... (next time todo)
			transform.position = Vector3.Slerp (transform.position, newPosition, Time.deltaTime * 10);

			Quaternion a = new Quaternion ();
			Quaternion b = new Quaternion ();
			a.SetLookRotation (transform.forward.normalized);
			Vector3 vDir = transform.position - prevPositon;
			b.SetLookRotation (vDir.normalized);
			transform.rotation = Quaternion.Slerp (a, b, Time.deltaTime);

			//transform.rotation = b;//Quaternion.Lerp (a, b, Time.deltaTime);//
		}
	}

	void Seperation(/*List<GameObject>*/){
		Vector3 vSteeringForce = new Vector3(0,0,0);
		if (gameMgrRef != null) {
			if (!GetComponent<EnemyFormation> ().spearationAngleSet) {
				int monsterCount = 0;
				int monsterIndex = 0;
				foreach (GameObject monster in gameMgrRef.monsterPool) {
					if (monster.activeSelf) {
						if (monster == gameObject) {
							monsterIndex = monsterCount;
						}
						monsterCount++;

					}
				}

				float theta = 180 / monsterCount;
				float rad_step = Mathf.PI / 180 * theta;

				float rad = rad_step * monsterIndex;

				/*
				if (bulletCount % 2 == 1)
					rad = -bulletCount / 2 * rad_step;
				else
					rad = (-bulletCount / 2 + 0.5f) * rad_step;
*/
				float c = Mathf.Cos(rad);
				float s = Mathf.Sin (rad);

				//GameObject bulletObj = Instantiate (bullet, fireTransform.position, fireTransform.rotation);
				Vector3 velocity = transform.forward;//firePos.forward;
				velocity.x = transform.forward.x * c - transform.forward.z * s;
				velocity.z = transform.forward.x * s + transform.forward.z * c;
				velocity.y = 0;//transform.position.y;

				Quaternion a = new Quaternion ();
				a.SetLookRotation (velocity);
				transform.rotation = a;

				GetComponent<EnemyFormation> ().spearationAngleSet = true;
			}
			/*
			foreach (GameObject monster in gameMgrRef.monsterPool) {
				if (monster.activeSelf) {
					
					if (monster != this.gameObject) {
						Vector3 vToAgent = transform.position - monster.transform.position;
						float fDistance = Vector3.Distance (transform.position, monster.transform.position);
						if(fDistance > 0)
							vSteeringForce += vToAgent.normalized / fDistance; 
					}

				}
			}
			*/
			vSteeringForce = transform.forward * Speed;
			transform.position += vSteeringForce;
		}
	}

	void Alignment(){
		Vector3 vAvgHeading = new Vector3(0,0,0);
		int neighborCount = 0;
		foreach (GameObject monster in gameMgrRef.monsterPool) {
			if (monster.activeSelf) {
				if (monster != this.gameObject) {
					vAvgHeading += monster.transform.forward;
					neighborCount++;
				}
			}
		}

		if (neighborCount > 0) {
			vAvgHeading /= (float)neighborCount;
			vAvgHeading -= transform.forward;
		}
		transform.position += vAvgHeading;
	}

	Vector3 Seek(Vector3 vTargetPos){
		Vector3 vDesiredVelocity = vTargetPos - transform.position;
		vDesiredVelocity = vDesiredVelocity.normalized * Speed; //velocity
		vDesiredVelocity.y = 0;
		return vDesiredVelocity;
	}

	void DashToPlayer()
	{
		GameObject player = GameObject.Find ("Player");
		if (player != null) {
			Vector3 velocity = Seek (player.transform.position);
			transform.position += velocity;

			Quaternion a = new Quaternion ();
			a.SetLookRotation (velocity);
			transform.rotation = a;
		}
	}

	void Cohesion()
	{
		Vector3 vCenterOfMass = new Vector3 (0, 0, 0);
		Vector3 vSteeringForce = new Vector3(0,0,0);
		int neighborCount = 0;
		foreach (GameObject monster in gameMgrRef.monsterPool) {
			if (monster.activeSelf) {
				if (monster != this.gameObject) {
					vCenterOfMass += transform.position;
					neighborCount++;
				}
			}
		}

		if (neighborCount > 0) {
			//vCenterOfMass /= (float)neighborCount;
			GameObject bossObj = GameObject.FindWithTag("BOSS");
			if(bossObj != null)
				vCenterOfMass = bossObj.transform.position;
			Debug.Log ("vCenterOfMass " + vCenterOfMass.ToString () + " neighborCount : " + neighborCount);

			//seek for vCenterOfMass;
			vSteeringForce = Seek (vCenterOfMass);
			//Debug.Log ("vSteeringForce " + vSteeringForce.ToString ());
		}
		transform.position += vSteeringForce;


		Quaternion a = new Quaternion ();
		a.SetLookRotation (vSteeringForce);
		transform.rotation = a;
	}


	void OnTriggerEnter(Collider other)
	{
		//Debug.Log ("OnTriggerEnter" + other.gameObject.name);
		if (other.gameObject.tag == "Bullet") {
			Destroy (other.gameObject);
			if (GetComponent<EnemyState> ().enemyState == EnemyState.eEnemyState.eNormalState) {
				OnDamaged ();

			}
		}
	}

	public void OnDamaged()
	{
		if (GetComponent<EnemyState> ().enemyState == EnemyState.eEnemyState.eImmortalState)
			return;
		if (healthBar != null) {
			if (healthBar.currHealth > 0) {
				healthBar.currHealth -= 1;
				if (healthBar.currHealth <= 0) {
					DisableAndAddScore ();

					Instantiate(explosion, transform.position, transform.rotation);
				}
			}
		}
	}


	void DisableAndAddScore()
	{
		if (GetComponent<EnemyState> ().enemyState != EnemyState.eEnemyState.eImmortalState)
		{
			this.gameObject.SetActive (false);

			GameUI gameUI = Camera.main.GetComponent<GameUI> ();
			if(gameUI != null)
				gameUI.AddScore (1);
		}
	}
}
