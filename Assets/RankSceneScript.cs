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
	Dictionary<string, IScore> dictionaryScore = new Dictionary<string, IScore>();
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
			//daily는 최신 alltime은 아닌듯
			PlayGamesPlatform.Instance.LoadScores("CgkIx4Xil-YYEAIQCQ", LeaderboardStart.PlayerCentered, 20, 
				LeaderboardCollection.Public, LeaderboardTimeSpan.Weekly, (data) => {
					myScores += "CallBack called\n";
					if (data.Scores.Length > 0) {
						
						SetScores(data.Scores);

				} else
					{
					myScores += "NoScores Loaded";
					//Debug.Log ("No scores loaded");

						GameObject.Find ("Canvas").GetComponentInChildren<Text> ().text = myScores;
					}
			});
		} else {
			myScores = "Noauthenticated\n";

			GameObject.Find ("Canvas").GetComponentInChildren<Text> ().text = myScores;
		}
	}

	public void SetScores(IScore[] scores)
	{
		//string myScores = "Leaderboard:\n";

		string[] userID = new string[scores.Length];
		int index = 0;
		foreach (IScore score in scores) {
			userID [index] = score.userID;
			dictionaryScore [score.userID] = score;
			//myScores += "\t" + "[" + score.rank + "] "  + score.userID + " " + score.formattedValue + " " + score.date + "\n";

			index++;
		}


		PlayGamesPlatform.Instance.LoadUsers (userID, (users) => {
			SetUsers (users);
		});

	
		//GameObject.Find ("Canvas").GetComponentInChildren<Text> ().text = myScores;
	}

	public void SetUsers(IUserProfile[] userProfiles)
	{
		string myScores = "Leaderboard:\n";
		foreach (IUserProfile userprofile in userProfiles) {
			if(dictionaryScore.ContainsKey(userprofile.id)){
				IScore score = dictionaryScore[userprofile.id];
				myScores += "\t" + "[" + score.rank + "] "  +userprofile.userName + " " + score.formattedValue + " " + score.date + "\n";
				//myScores += userprofile.userName;
			}
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
