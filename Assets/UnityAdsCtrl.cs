using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Advertisements;


public class UnityAdsCtrl : MonoBehaviour {
	bool visible = false;

	//ShowOptions _ShowOpt = new ShowOptions { resultCallback = OnAdsShowResultCallBack };//new ShowOptions();

	// Use this for initialization
	void Start () {
		Advertisement.Initialize ("1226554", false);
		//var options = new ShowOptions { resultCallback = OnAdsShowResultCallBack };
		//Advertisement.Show("rewardedVideo", options);

		//gameObject.transform.localScale = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Show(bool bShow)
	{
		visible = bShow;
		if (visible)
			gameObject.transform.localScale = new Vector3 (1, 1, 1);
		else {
			gameObject.transform.localScale = new Vector3 (0, 0, 0);
			Debug.Log ("Show false");
		}
	}

	private void OnAdsShowResultCallBack(ShowResult result)
	{
		if (result == ShowResult.Finished) {
			int gamePoint = PlayerPrefs.GetInt ("GamePoint");
			gamePoint += 10;
			PlayerPrefs.SetInt ("GamePoint", gamePoint);
			gameObject.SetActive (false);
		}
	}


	public void OnClickShowAds()
	{
		var _ShowOpt = new ShowOptions { resultCallback = OnAdsShowResultCallBack };//new ShowOptions();
		Advertisement.Show (null, _ShowOpt);
	}
	public void OnClickCloseButton()
	{
		/*
		this.visible = false;
		gameObject.SetActive (false);
		gameObject.transform.localScale = new Vector3 (0, 0, 0);
		transform.localScale= new Vector3 (0, 0, 0);
		*/
		GameObject gameobject = GameObject.FindWithTag ("Ads");
		if (gameobject != null) {
			gameobject.transform.localScale = new Vector3 (0, 0, 0);
		}
	}
}

/*
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsExample : MonoBehaviour
{
	public void ShowRewardedAd()
	{
		if (Advertisement.IsReady("rewardedVideo"))
		{
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show("rewardedVideo", options);
		}
	}

	private void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log("The ad was successfully shown.");
			//
			// YOUR CODE TO REWARD THE GAMER
			// Give coins etc.
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}
}
*/