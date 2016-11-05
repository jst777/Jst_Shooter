using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
	public InputField stageInputField;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClickStartButton(){
		//file DB 
		/*
		GameMgr gameMgr = GameObject.Find ("Camera").GetComponent<GameMgr> ();
		if (gameMgr) {
			PlayerPrefs.SetInt ("COLUM_COUNT", ++gameMgr.columnCount);
			PlayerPrefs.SetInt ("STAGE_COUNT", ++gameMgr.currentStage);
		}
		*/

		SceneManager.LoadScene ("JstScene");
	}

	public void OnClickStartWithStageButton()
	{
		//GameMgr gameMgr = GameObject.Find ("Camera").GetComponent<GameMgr> ();
		//gameMgr.currentStage = 1;
		int currentStage = 0;
		int.TryParse (stageInputField.text, out currentStage);
		PlayerPrefs.SetInt ("STAGE_COUNT", currentStage);
		SceneManager.LoadScene ("JstScene");
	}

	public void OnClickResetStageButton()
	{
		PlayerPrefs.SetInt ("STAGE_COUNT", 1);
		SceneManager.LoadScene ("JstScene");
	}
}
