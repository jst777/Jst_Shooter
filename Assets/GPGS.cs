using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using GooglePlayGames.BasicApi;

using UnityEngine.SocialPlatforms;


/*
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	protected static T instance = null;
	public static T GetInstance
	{
		get
		{
			if (instance == null)
			{
				instance = new T;//FindObjectOfType(typeof(T)) as T;

				if (instance == null)
				{
					Debug.Log("Nothing" + instance.ToString());
					return null;
				}
			}
			return instance;
		}
	}
}
*/


public class GPGSMng// : Singleton<GPGSMng>
{
	/// <summary>
	/// 현재 로그인 중인지 체크
	/// </summary>
	///
	public bool bLogin
	{
		get;
		set;
	}

	private static GPGSMng instance = null;

	public static GPGSMng GetInstance()
	{
		if(instance == null)
		{
			instance = new GPGSMng();
			instance.InitializeGPGS ();
		}
		return instance;
	}

	public void OnTouch()
	{
	}

	/// <summary>
	/// GPGS를 초기화 합니다.
	/// </summary>
	public void InitializeGPGS()
	{
		bLogin = false;

		PlayGamesPlatform.Activate();
	}

	/// <summary>
	/// GPGS를 로그인 합니다.
	/// </summary>
	public void LoginGPGS()
	{
		// 로그인이 안되어 있으면
		if (!Social.localUser.authenticated) {
			Social.localUser.Authenticate (LoginCallBackGPGS);
			Debug.Log ("LoginGPGS" + Social.localUser.authenticated.ToString());
		}
	}

	/// <summary>
	/// GPGS Login Callback
	/// </summary>
	/// <param name="result"> 결과 </param>
	public void LoginCallBackGPGS(bool result)
	{
		bLogin = result;
	}

	/// <summary>
	/// GPGS를 로그아웃 합니다.
	/// </summary>
	public void LogoutGPGS()
	{
		// 로그인이 되어 있으면
		if (Social.localUser.authenticated)
		{
			((GooglePlayGames.PlayGamesPlatform)Social.Active).SignOut();
			bLogin = false;
			Debug.Log ("LogoutGPGS" + Social.localUser.authenticated.ToString());
		}
	}

	/// <summary>
	/// GPGS에서 자신의 프로필 이미지를 가져옵니다.
	/// </summary>
	/// <returns> Texture 2D 이미지 </returns>
	public Texture2D GetImageGPGS()
	{
		if (Social.localUser.authenticated)
			return Social.localUser.image;
		else
			return null;
	}

	/// <summary>
	/// GPGS 에서 사용자 이름을 가져옵니다.
	/// </summary>
	/// <returns> 이름 </returns>
	public string GetNameGPGS()
	{
		if (Social.localUser.authenticated)
			return "authenticated" + Social.localUser.userName;
		else
			return "nonauthenticated";
			//return null;

	}

	public void ReportScore(int highScore)
	{
		if (Social.localUser.authenticated) {
			//lambda
			Social.ReportScore (highScore, "CgkIx4Xil-YYEAIQCQ", (bool suc) => { 
				if (suc)
				{
					//실제 랭킹은 어디서 보나
					//PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkIx4Xil-YYEAIQCQ");//ScoreBoard");
				}
			});
		}
	}

	public void LoadScores()
	{
		Social.LoadScores("CgkIx4Xil-YYEAIQCQ", scores => {
			if (scores.Length > 0) {
				Debug.Log ("Got " + scores.Length + " scores");
				string myScores = "Leaderboard:\n";
				foreach (IScore score in scores)
					myScores += "\t" + score.userID + " " + score.formattedValue + " " + score.date + "\n";
				Debug.Log (myScores);
			}
			else
				Debug.Log ("No scores loaded");
		});
	}

}