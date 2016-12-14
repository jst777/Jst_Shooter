using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum ButtonType
{
	eTopButton,
	eBottomButton,
	eLeftButton,
	eRightButton,
	eButtonMax,
};
public class UIMgr : MonoBehaviour {
	//private bool buttonList[eButtonMax];
	//private bool topMove = false;
	//private bool bottomMove = false;
	//private bool leftMove = false;
	//private bool rightMove = false;

	//private bool leftRotate = false;
	//private bool rightRotate = false;
	public GameObject playerObject;
	private Camera camera;

	public List<string> buttonList;


	//
	public GameObject gameOver;
	public GameObject stageClear;

	// Use this for initialization
	void Start () {
		camera = GameObject.Find ("Camera").GetComponent<Camera> ();
		if (camera != null) {
			Debug.Log ("found Camera");
		}
		gameOver = GameObject.Find ("GameOver");
		if (gameOver != null) {
			gameOver.SetActive (false);
		}

		stageClear = GameObject.Find ("StageClear");
		if (stageClear != null) {
			stageClear.SetActive (false);
		}

		//disable buttons
		buttonList.Add ("TopButton");
		buttonList.Add ("BottomButton");
		buttonList.Add ("LeftButton");
		buttonList.Add ("RightButton");
		buttonList.Add ("LeftRotateButton");
		buttonList.Add ("RightRotateButton");
		//buttonList.Add ("FireButton");

		foreach (string buttonName in buttonList) {
			GameObject buttonObj = GameObject.Find (buttonName);
			if (buttonObj != null) {
				buttonObj.SetActive (false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}



	public void OnGameRetryButton()
	{
		GameMgr gameMgr = GameObject.Find ("Camera").GetComponent<GameMgr> ();
		//if (gameMgr) {
			//PlayerPrefs.SetInt ("COLUM_COUNT", 0);
			//PlayerPrefs.SetInt ("STAGE_COUNT", 1);
		//}

		SceneManager.LoadScene ("JstScene");
	}

	public void OnStageClearButton()
	{
		//file DB 
		GameMgr gameMgr = GameObject.Find ("Camera").GetComponent<GameMgr> ();
		if (gameMgr) {
			//PlayerPrefs.SetInt ("COLUM_COUNT", ++gameMgr.columnCount);
			PlayerPrefs.SetInt ("STAGE_COUNT", ++gameMgr.currentStage);
		}

		SceneManager.LoadScene ("JstScene");
	}

	public void OnClickMainMenuButton()
	{
		SceneManager.LoadScene ("MainMenuScene");
	}

}
