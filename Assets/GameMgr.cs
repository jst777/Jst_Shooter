using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour {
	public Transform[] points;

	public GameObject monsterPrefab;
	public List<GameObject> monsterPool = new List<GameObject>();

	public float creationTime = 2.0f;
	//private int maxMonster = 1;
	public int columnCount = 3;
	public bool isGameOver = false;
	public int goalStep = 3;
	public int currentStep{
		get;set;
	}

	public int currentStage = 1;// {
	//	get;
	//	set;
	//}

	public static GameMgr instance = null;

	public int maxMonster {
		get;
		set;
	}

	void Awake()
	{
		instance = this;
		maxMonster = columnCount * (columnCount - 1);

	}

	// Use this for initialization
	void Start () {
		points = GameObject.Find ("SpawnPoint").GetComponentsInChildren<Transform> ();
		currentStep = 1;


		int columnCountPref = PlayerPrefs.GetInt ("COLUM_COUNT");
		if (columnCountPref > 0) {
			columnCount = columnCountPref;
		}

			currentStage = 1;
			int stageCountRef = PlayerPrefs.GetInt ("STAGE_COUNT");
			if (stageCountRef > 0) {
				currentStage = stageCountRef;
			}
		
		int checkStage = currentStage / (int)EnemyFire.eFireType.eMaxFireType;
		currentStep = 1;
		columnCount = 1 + checkStage;

		RespawnNextMonster ();
	}

	public void UpdateScore()
	{
		//Debug.Log ("Delegate test");
		//int currentScore = PlayerPrefs.GetInt("TOT_SCORE");

		foreach (GameObject monster in monsterPool) {
			if (monster.activeSelf) {
				return;
			}
		}

		if (currentStep < goalStep)
			RespawnNextMonster ();
		else { //game over
			GameObject uiMgrobj = GameObject.Find ("UIManager");
			if (uiMgrobj != null) {
				UIMgr uiMgr = uiMgrobj.GetComponentInChildren<UIMgr> ();
				if (uiMgr != null) {
					Text txt = uiMgr.stageClear.GetComponentInChildren<Text> ();
					if (txt != null) {
						int stage = currentStage;
						txt.text = "Stage "+ stage.ToString() + "Clear" + "Next Stage GO?";

					}
					uiMgr.stageClear.SetActive (true);


					CameraControl camControl = Camera.main.GetComponent<CameraControl> ();
					if (camControl != null) {
						camControl.SetEndingCameraAction ();
					}

					GameObject player = GameObject.Find ("Player");
					if(player!= null)
					{
						player.GetComponent<FireCtrl> ().stopFire = true;
					}
							

				}
			}
		}
	}

	public void RespawnNextMonster()
	{
		//factorial 증
		columnCount++;
		currentStep++;
		maxMonster = 0;
		for (int i = 0; i < columnCount; i++) {
			maxMonster += i;
		}

		monsterPool.Clear ();

		//int halfPoints = points.Length / 2;
		int startPoints = points.Length - 3;
		int random = 0;
		Random.InitState ((int)System.DateTime.Now.Ticks);
		//random = Random.Range (1, halfPoints);
		random = Random.Range (1, 100);
		random = (random % startPoints) + 3;
		//Debug.Log ("halfPoints and random = " + halfPoints.ToString () + "," + random.ToString ());

		int startPos = random;
		Random.InitState ((int)System.DateTime.Now.Ticks);
		random = Random.Range (1, 100);
		random = (random % 2) + 1;
		//temporary 2 dest
		int destPos = random;
		Random.InitState ((int)System.DateTime.Now.Ticks);
		float randomFormation = Random.Range (0, (float)EnemyFormation.eFormation.eMaxFormation);

		//EnemyFormation.eFormation randomFormation = randomFormation;//EnemyFormation.eFormation.eMaxFormation;

		for (int i = 0; i < maxMonster; i++) {
			GameObject monster = (GameObject)Instantiate (monsterPrefab);
			monster.name = "Monster_" + i.ToString ();
			if (i == 0) {
				monster.tag = "BOSS";
			}
			monster.GetComponent<EnemyScript>().startIndex = startPos;
			monster.GetComponent<EnemyScript>().destinationIndex = destPos;

			monster.GetComponent<EnemyFormation>().formation = (EnemyFormation.eFormation)randomFormation;

			//string[] splitString = monster.name.Split (new string[] { "_", "\n" }, System.StringSplitOptions.None);
			//Debug.Log(splitString[1]);

			//if(i==0)
			//	monster.tag = "Boss";


			monster.SetActive (false);
			monsterPool.Add (monster);

			if (points.Length > 0) {
				StartCoroutine (this.CreateMonster (i));
			}

			//Debug.Log ("points size =  " + points.Length);

			//monster.transform.position = points [1].position;
		}
	}

	IEnumerator CreateMonster(int idx)
	{
		yield return new WaitForSeconds (creationTime);

		if (isGameOver)
			yield break;

		//int idx = 1;
		foreach (GameObject monster in monsterPool) {
			if (!monster.activeSelf) {
				//idx = Random.Range (1, points.Length);

				//monster.transform.position = points [2].position;

				monster.SetActive (true);

				HealthBar healthBar = monster.GetComponent<HealthBar> ();
				if(healthBar != null)
				{
					if (monster.tag == "BOSS") {
						healthBar.maxHealth = healthBar.currHealth = 5;
					} else {
						healthBar.maxHealth = healthBar.currHealth = 1;
					}
				}

				//idx++;
				break;
			}
		}
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
