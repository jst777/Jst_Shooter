using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour {
	public enum eEnemyState{
		eImmortalState,
		eNormalState,
		eMaxState
	}

	public eEnemyState enemyState {
		get;
		set;
	}

	// Use this for initialization
	void Start () {
		enemyState = eEnemyState.eImmortalState;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
