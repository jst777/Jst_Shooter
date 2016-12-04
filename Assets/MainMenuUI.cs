using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
	public InputField stageInputField;
	public InputField chatInputField;
	public Text	chatText;


	public Text Login_Label = null;
	public Text User_Label = null;
	//public UITexture User_Texture = null;

	// Use this for initialization
	void Start () {
		GPGSMng.GetInstance ().OnTouch ();//InitializeGPGS();

		if(GPGSMng.GetInstance().bLogin == false)
		{
			//GPGSMng.GetInstance().LoginGPGS(); // 로그인
		}
	}
	
	// Update is called once per frame
	void Update () {
		//if (GPGSMng.GetInstance().bLogin == true) 
		{
			SettingUser ();
		}
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

	public void OnClickRankButton()
	{
		//PlayerPrefs.SetInt ("STAGE_COUNT", 1);
		//SceneManager.LoadScene ("JstScene");
		SceneManager.LoadScene ("Rank");
	}

	public void OnClickShopButton()
	{
		SceneManager.LoadScene ("ShopScene");
	}

	public void OnClickChatButton()
	{
		//ServerManager mgr = Camera.main.GetComponent<ServerManager> ();
		//mgr.EmitChat ("jst", chatInputField.text, System.DateTime.Now);

		if (GPGSMng.GetInstance ().bLogin == false) {
			GPGSMng.GetInstance ().LoginGPGS (); // 로그인
		} else {
			GPGSMng.GetInstance ().LogoutGPGS (); // 로그인
		}
	}

	public void AddChatText(string name, string msg, string date)
	{
		chatText.text += "[" + name + "]  "  + msg + "\n";
	}

	void SettingUser()
	{
		//if (User_Texture.mainTexture != null)
		//if(User_Label!= null)//.text != null)
		//	return;

		User_Label.enabled = true;
		//User_Texture.enabled = true;

		User_Label.text = "name=" + GPGSMng.GetInstance().GetNameGPGS();
		//User_Texture.mainTexture = GPGSMng.GetInstance.GetImageGPGS();
	}
}
