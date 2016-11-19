using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
	public InputField stageInputField;
	public InputField chatInputField;
	public Text	chatText;


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

	public void OnClickShopButton()
	{
		SceneManager.LoadScene ("ShopScene");
	}

	public void OnClickChatButton()
	{
		ServerManager mgr = Camera.main.GetComponent<ServerManager> ();
		mgr.EmitChat ("jst", chatInputField.text, System.DateTime.Now);

		//chatText.text += chatInputField.text + "\n";
		//chatText.text = "hello \n hi \n hello there \n";
	}

	public void AddChatText(string name, string msg, string date)
	{
		chatText.text += "[" + name + "]  "  + msg + "\n";
	}
}
