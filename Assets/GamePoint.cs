using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ShowGamePoint ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void AddGamePoint(int _gamePoint)
	{
		int gamePoint = PlayerPrefs.GetInt ("GamePoint");
		gamePoint += _gamePoint;
		PlayerPrefs.SetInt ("GamePoint", gamePoint);

		Text textGamePoint = GetComponentInChildren<Text> ();
		if(textGamePoint != null)
			textGamePoint.text = "GamePoint :" + gamePoint.ToString ();
	}

	public void SpendGamePoint(int _gamePoint)
	{
		int gamePoint = PlayerPrefs.GetInt ("GamePoint");
		if (gamePoint >= _gamePoint) {
			gamePoint -= _gamePoint;
			
			PlayerPrefs.SetInt ("GamePoint", gamePoint);

			Text textGamePoint = GetComponentInChildren<Text> ();
			if (textGamePoint != null)
				textGamePoint.text = "GamePoint :" + gamePoint.ToString ();
		}
	}

	public void ShowGamePoint()
	{
		int gamePoint = PlayerPrefs.GetInt ("GamePoint");
		GetComponentInChildren<Text> ().text = "GamePoint :" + gamePoint.ToString ();
	}
}
