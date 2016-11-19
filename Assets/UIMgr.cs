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

	public bool topMove { get; set; }
	public bool bottomMove { get; set; }
	public bool leftMove { get; set; }
	public bool rightMove { get; set; }
	public bool leftRotate { get; set; }
	public bool rightRotate { get; set; }


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
		
		float xx = 0.0f;
		float zz = 0.0f;


		if (leftRotate) {
			playerObject.transform.Rotate (-1 * Vector3.up * Time.deltaTime * 100, Space.World);
		} else if (rightRotate) {
			playerObject.transform.Rotate(Vector3.up * Time.deltaTime * 100, Space.World);
		}

		if (topMove || bottomMove || leftMove || rightMove) {
			if (topMove) {
				xx = 1.0f;
			} else if (bottomMove) {
				xx = -1.0f;
			}
			if (leftMove) {
				zz = -1.0f;
			} else if (rightMove) {
				zz = 1.0f;
			}
			Vector3 lookDir = playerObject.transform.forward * xx + zz * playerObject.transform.right;//xx * Vector3.forward + zz * Vector3.right;

			playerObject.transform.position += (lookDir.normalized * 5 * Time.deltaTime);
			Debug.Log (playerObject.transform.position.ToString ());
		}

	}

	public void OnPointerDownTopArrow()
	{
		topMove = true;
		//Debug.Log ("OnPointerDownTopArrow");
	}
	public void OnPointerUpTopArrow()
	{
		topMove = false;
		//Debug.Log ("OnPointerUpTopArrow");
	}

	public void OnPointerDownBottomArrow()
	{
		bottomMove = true;
	}
	public void OnPointerUpBottomArrow()
	{
		bottomMove = false;
	}

	public void OnPointerDownLeftArrow()
	{
		leftMove = true;
	}
	public void OnPointerUpLeftArrow()
	{
		leftMove = false;
	}
	public void OnPointerDownRightArrow()
	{
		rightMove = true;
	}
	public void OnPointerUpRightArrow()
	{
		rightMove = false;
	}

	public void OnPointerDownLeftRotate()
	{
		leftRotate = true;
	}
	public void OnPointerUpLeftRotate()
	{
		leftRotate = false;
	}
	public void OnPointerDownRightRotate()
	{
		rightRotate = true;
	}
	public void OnPointerUpRightRotate()
	{
		rightRotate = false;
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
			PlayerPrefs.SetInt ("COLUM_COUNT", ++gameMgr.columnCount);
			PlayerPrefs.SetInt ("STAGE_COUNT", ++gameMgr.currentStage);
		}

		SceneManager.LoadScene ("JstScene");
	}

	public void OnClickMainMenuButton()
	{
		SceneManager.LoadScene ("MainMenuScene");
	}

}
