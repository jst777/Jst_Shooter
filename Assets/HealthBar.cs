using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

	public GameObject healthBarPrefab; //체력게이지프리팹을담음
	private GameObject healthBarObj;//프리팹으로 생성한 인스턴스를 담음
	public float currHealth{//현재의체력//
		get; set;
	}
	public float maxHealth{//최대체력
		get;set;
	}
	int healthBarWidth;//체력게이지의폭

	// Use this for initialization
	void Start () {
		if (tag == "Player") {
			maxHealth = 10;
			currHealth = 10;
		} else if (tag == "Fellow") {
			maxHealth = 5;
			currHealth = 5;
		} else {
			float levelHPRate = 1;
			GameMgr gamemgr = Camera.main.GetComponent<GameMgr> ();
			if (gamemgr != null) {
				levelHPRate = (gamemgr.currentStage / (int)EnemyFormation.eFormation.eMaxFormation);
			}
			if (tag == "BOSS") {
				maxHealth = 10 + 10 * levelHPRate; 
				currHealth = 10 + 10 * levelHPRate;
			} else if (tag == "Turret") {
				maxHealth = 5 + 5 * levelHPRate; 
				currHealth = 5 + 5 * levelHPRate;
			} else if (tag == "Enemy") {
				maxHealth = 1 + 1 * levelHPRate; 
				currHealth = 1 + 1 * levelHPRate;
			}
		}

	}

	// Update is called once per frame
	void Update () {
		float fHpRate = currHealth / maxHealth;
		healthBarPrefab.transform.localScale = new Vector3 (fHpRate, 1, 1);
		/*
		//오브젝트와의 정렬
		healthBarObj.transform.position =
			Camera.main.WorldToViewportPoint(transform.position);//월드좌표에서 뷰포트 좌표로 변환
		Vector3 pos = healthBarObj.transform.position;
		pos.y += 0.13f;
		healthBarObj.transform.position = pos;

		float healthPercent = currHealth/maxHealth;
		if(healthPercent < 0f)
		{
			healthPercent = 0f;
		}
		healthBarWidth = (int)healthPercent * 20;
		//빌드에러 난다 
		//healthBarObj.guiTexture.pixelInset =
		//	new Rect(10,10, healthBarWidth, 5);
		*/
	}
}
