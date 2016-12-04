using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class RankSceneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		LoadScores ();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void OnClickMainMenuButton()
	{
		//ShopCost shopCost = GameObject.Find ("BuyButton1").GetComponentInChildren<ShopCost> ();
		//GameObject.Find ("GamePointPanel").GetComponentInChildren<GamePoint> ().SpendGamePoint (shopCost.cost);

		SceneManager.LoadScene ("MainMenuScene");
	}

	public void LoadScores()
	{
		string myScores = "Leaderboard:\n";
		if (Social.localUser.authenticated) {
			myScores += "authenticated\n";
			//Social.LoadScores ("CgkIx4Xil-YYEAIQCQ", scores => {
			//var lb =  Social.CreateLeaderboard();
			PlayGamesPlatform.Instance.LoadScores("CgkIx4Xil-YYEAIQCQ", LeaderboardStart.TopScores, 1, 
				LeaderboardCollection.Public, LeaderboardTimeSpan.AllTime, (data) => {
					myScores += "CallBack called\n";
					if (data.Scores.Length > 0) {
						Debug.Log ("Got " + data.Scores.Length + " scores");
					//myScores += "playerCount" + scores.Length.ToString();
						foreach (IScore score in data.Scores)
						myScores += "\t" + score.userID + " " + score.formattedValue + " " + score.date + "\n";
					//Debug.Log (myScores);


				} else
					myScores += "NoScores Loaded";
					//Debug.Log ("No scores loaded");
			});
		} else {
			myScores = "Noauthenticated\n";
		}

		GameObject.Find ("Canvas").GetComponentInChildren<Text> ().text = myScores;
	}

	public void OnClickRankButton()
	{
		if (Social.localUser.authenticated) {
			PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkIx4Xil-YYEAIQCQ");//ScoreBoard");
		}
	}
}
