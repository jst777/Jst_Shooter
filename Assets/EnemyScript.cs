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

    public int destinationIndex {
		get;
		set;
	}
	public int startIndex {
		get;
		set;

	}
	public Transform[] points;

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
			points = GameObject.Find ("SpawnPoint").GetComponentsInChildren<Transform> ();
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
		transform.position = points [startIndex].position;

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
	/*
	void CalculateOffsetPos(){

		string[] splitString = name.Split(new string[] { "_", "\n"}, System.StringSplitOptions.None);

		if (splitString.Length >= 2 && isSetPos == false) {
			int idx = 0;
			//Debug.Log ("splitString.length" + splitString.Length.ToString() + splitString[1].ToString());		
			if (int.TryParse (splitString [1], out idx)) {
			}
				
			if (idx != 0) {
				
				int sum = 0;
				int selectedCol = 0;

				for (int col = 1; col < 100; col++) {
					sum += col;
					if (idx < sum) {
						selectedCol = col;
						break;	
					}
				}

				sum -= selectedCol;
				selectedCol -= 1;


				float centerIndex = sum + (selectedCol) * 0.5f;
				float pos = idx - centerIndex;

				//Debug.Log ("[x][z] = " + pos.ToString () + " " + centerIndex.ToString () + " " + selectedCol.ToString ());

			
				vOffsetToBoss = new Vector3 (offsetXZ * pos, 0, offsetXZ * selectedCol);
			} else {
				//tag = "BOSS"; //set index 0 to boss
			}

			vDestination = points[destinationIndex].transform.position + vOffsetToBoss;
		}
	}
	*/
	
	// Update is called once per frame
	void Update () {
		if (points == null)
			return;
		if (points.Length == 0)
			return;
		if (points [destinationIndex] == null || points [startIndex] == null)
			return;

		float fDistance = Vector3.Distance(vDestination, transform.position); 

		if (tag == "BOSS") {
			if (fDistance > 1) {
				Vector3 vDir = points [destinationIndex].position - points [startIndex].position;
				
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
			transform.position = Vector3.Slerp (transform.position, points[destinationIndex].position, Speed * Time.deltaTime);

			Velocity = transform.forward;
			Velocity = Velocity.normalized * Speed;
			//transform.position = transform.position + Velocity * Time.deltaTime;
		}
		else {
			GameObject bossObj = GameObject.FindWithTag ("BOSS");
			if (bossObj != null) {
				float fDistanceToBoss = Vector3.Distance (bossObj.transform.position, transform.position);

				if (gameMgrRef != null) {
					//test
					//if (gameMgrRef.currentStep > 2) //2 - first
					OffsetPursuit (bossObj, vOffsetToBoss);
					/*else {
						if(fDistanceToBoss > 10)
						{
							Cohesion ();
						}
						else{
							Speration();
						}
						//Speration ();
					}
					*/
				}
			} else {
				GameObject playerObj = GameObject.Find ("Player");
				if (playerObj != null) {
					//transform.position = Vector3.Slerp (transform.position, playerObj.transform.position, Speed * Time.deltaTime);
					transform.position+= (transform.forward * Speed * Time.deltaTime * 10);

					Plane[] planes = GeometryUtility.CalculateFrustumPlanes (Camera.main);
					//out of sight
					if (!GeometryUtility.TestPlanesAABB (planes, gameObject.GetComponent<Collider> ().bounds)) {
						DisableAndAddScore ();
					}
				}
			}
		}
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

	void Speration(/*List<GameObject>*/){
		Vector3 vSteeringForce = new Vector3(0,0,0);
		if (gameMgrRef != null) {
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
	}

	Vector3 Seek(Vector3 vTargetPos){
		Vector3 vDesiredVelocity = vTargetPos - transform.position;
		vDesiredVelocity = vDesiredVelocity.normalized * Speed; //velocity

		return vDesiredVelocity;
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
		if (healthBar != null) {

			healthBar.currHealth -= 1;
			if (healthBar.currHealth <= 0) {
				DisableAndAddScore ();
			}
		}
	}


	void DisableAndAddScore()
	{
		if (GetComponent<EnemyState> ().enemyState == EnemyState.eEnemyState.eNormalState) {
			this.gameObject.SetActive (false);

			GameUI gameUI = Camera.main.GetComponent<GameUI> ();
			if(gameUI != null)
				gameUI.AddScore (1);
		}
	}
}
