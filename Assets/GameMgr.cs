using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour {
	public Transform[] spawnPoints;
	public Transform[] destPoints;

	public GameObject monsterPrefab;
	public List<GameObject> monsterPool = new List<GameObject>();

	public float creationTime = 2.0f;
	//private int maxMonster = 1;
	//public int columnCount = 3;
	public bool isGameOver = false;
	public int goalStep = 3;
	//public int currentStep{
	//	get;set;
	//}

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
		//maxMonster = columnCount * (columnCount - 1);
	}

	// Use this for initialization
	void Start () {
		spawnPoints = GameObject.Find ("SpawnPoint").GetComponentsInChildren<Transform> ();
		destPoints = GameObject.Find ("DestinationPoint").GetComponentsInChildren<Transform> ();

		currentStage = 1;
		int stageCountRef = PlayerPrefs.GetInt ("STAGE_COUNT");
		if (stageCountRef > 0) {
			currentStage = stageCountRef;
		}

		CalculateMonsterCount ();

		RespawnNextMonster ();
	}

	void CalculateMonsterCount()
	{
		//스테이지 변수 갯수
		int variableCount = (int)EnemyFire.eFireType.eMaxFireType * (int)EnemyFormation.eFormation.eMaxFormation;
		int checkStage = currentStage / variableCount;
		//columnCount = 1 + checkStage;
		//boss와 부하2마리부터 시작
		maxMonster = 3;
		maxMonster += (checkStage * 2); //2마리씩 늘어남

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

		//if (currentStep < goalStep)
		//	RespawnNextMonster ();
		//else 
		{ //game over
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

					GameObject unityAdsPanel = GameObject.Find ("UnityAdsPanel");
					if (unityAdsPanel != null) {
						unityAdsPanel.GetComponent<UnityAdsCtrl> ().Show (true);
					}

					CameraControl camControl = Camera.main.GetComponent<CameraControl> ();
					if (camControl != null) {
						camControl.SetEndingCameraAction ();
					}

					GameObject player = GameObject.Find ("Player");
					if(player!= null)
					{
						player.GetComponent<FireCtrl> ().stopFire = true;
						//lock move and camera
						player.GetComponent<Move> ().enabled = false;
						//lock collision
						player.GetComponent<PlayerCtrl> ().enabled = false;

						GameObject gamepointpanel = GameObject.Find ("GamePointPanel");
						if (gamepointpanel != null) {
							int addedGamePoint = 0;
							addedGamePoint = maxMonster;


							gamepointpanel.GetComponent<GamePoint> ().AddGamePoint (addedGamePoint);

							//일단 게임포인트와 동일
							GPGSMng.GetInstance().ReportScore(currentStage);
						}
					}
				}
			}
		}
	}

	public void RespawnNextMonster()
	{

		monsterPool.Clear ();


		int random = 0;
		Random.InitState ((int)System.DateTime.Now.Ticks);
		random = (random % spawnPoints.Length);

		int startPos = random;
		Random.InitState ((int)System.DateTime.Now.Ticks);
		random = (random % destPoints.Length);
		int destPos = random;
		Random.InitState ((int)System.DateTime.Now.Ticks);
		float randomFormation = Random.Range (0, (float)EnemyFormation.eFormation.eMaxFormation);

		randomFormation = (currentStage - 1) / (int)EnemyFire.eFireType.eMaxFireType;

		//randomFormation = (float)EnemyFormation.eFormation.eCarrierFormation;
		//randomFormation = (float)EnemyFormation.eFormation.eArchFormation; //strange
		//randomFormation = (float)EnemyFormation.eFormation.eHakikFormation;
		//randomFormation = (float)EnemyFormation.eFormation.ePyramidFormation;
		//EnemyFormation.eFormation randomFormation = randomFormation;//EnemyFormation.eFormation.eMaxFormation;

		for (int i = 0; i < maxMonster; i++) {
			GameObject monster = (GameObject)Instantiate (monsterPrefab);
			monster.name = "Monster_" + i.ToString ();
			if (i == 0) {
				monster.tag = "BOSS";
			}
			monster.GetComponent<EnemyScript>().startIndex = startPos;
			monster.GetComponent<EnemyScript>().destinationIndex = destPos;

			monster.GetComponent<EnemyFormation> ().formation = (EnemyFormation.eFormation)randomFormation;

			//string[] splitString = monster.name.Split (new string[] { "_", "\n" }, System.StringSplitOptions.None);
			//Debug.Log(splitString[1]);

			//if(i==0)
			//	monster.tag = "Boss";


			monster.SetActive (false);
			monsterPool.Add (monster);

			if (spawnPoints.Length > 0) {
				StartCoroutine (this.CreateMonster (i));
			}

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
