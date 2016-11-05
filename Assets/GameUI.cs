using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
	public Text textScore;
	public Text textStage;
	private int currentScore = 0;

	public delegate void UpdateScore();
	public UpdateScore handler = null;

	// Use this for initialization
	void Start () {
		ClearScore ();

		//딜리게이트 설정 
		GameMgr gameMgr = GameObject.Find ("Camera").GetComponent<GameMgr> ();
		if (gameMgr) {
			handler = gameMgr.UpdateScore;
			DisplayStage (gameMgr.currentStage);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void DisplayScore()
	{
		textScore.text = "score <color=#ff0000>" + currentScore.ToString () + "</color>";
		PlayerPrefs.SetInt ("TOT_SCORE", currentScore);
		if(handler != null)
			handler ();
	}

	public void AddScore(int score)
	{
		currentScore += score;
		DisplayScore ();
	}

	public void ClearScore()
	{
		currentScore = 0;
		DisplayScore ();
	}

	public void DisplayStage(int stage)
	{
		textStage.text = "Stage <color=#ff0000>" + stage.ToString () + "</color>";
	}
}
