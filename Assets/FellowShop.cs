using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FellowShop : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClickBuyButton1()
	{
		if (PlayerPrefs.GetInt ("Fellow1") == 0) {
			ShopCost shopCost = GameObject.Find ("BuyButton1").GetComponentInChildren<ShopCost> ();
			GameObject.Find ("GamePointPanel").GetComponentInChildren<GamePoint> ().SpendGamePoint (shopCost.cost);
			//shopCost.cost
			PlayerPrefs.SetInt ("Fellow1", 1);
		}
	}
	public void OnClickBuyButton2()
	{
		if (PlayerPrefs.GetInt ("Fellow2") == 0) {
			ShopCost shopCost = GameObject.Find ("BuyButton2").GetComponentInChildren<ShopCost> ();
			GameObject.Find ("GamePointPanel").GetComponentInChildren<GamePoint> ().SpendGamePoint (shopCost.cost);

			PlayerPrefs.SetInt ("Fellow2", 1);
		}
	}
	public void OnClickBuyButton3()
	{
		if (PlayerPrefs.GetInt ("Fellow3") == 0) {
			ShopCost shopCost = GameObject.Find ("BuyButton3").GetComponentInChildren<ShopCost> ();
			GameObject.Find ("GamePointPanel").GetComponentInChildren<GamePoint> ().SpendGamePoint (shopCost.cost);

			PlayerPrefs.SetInt ("Fellow3", 1);
		}
	}

	public void OnClickBuyButton4()
	{
		if (PlayerPrefs.GetInt ("Fellow4") == 0) {
			ShopCost shopCost = GameObject.Find ("BuyButton4").GetComponentInChildren<ShopCost> ();
			GameObject.Find ("GamePointPanel").GetComponentInChildren<GamePoint> ().SpendGamePoint (shopCost.cost);

			PlayerPrefs.SetInt ("Fellow4", 1);
		}
	}

	public void OnClickBuyButton5()
	{
		ShopCost shopCost = GameObject.Find ("ShipButton1").GetComponentInChildren<ShopCost> ();
		GameObject.Find ("GamePointPanel").GetComponentInChildren<GamePoint> ().SpendGamePoint (shopCost.cost);

		PlayerPrefs.SetInt ("PlayerShip_Index", 1);
	}
	public void OnClickBuyButton6()
	{
		ShopCost shopCost = GameObject.Find ("ShipButton2").GetComponentInChildren<ShopCost> ();
		GameObject.Find ("GamePointPanel").GetComponentInChildren<GamePoint> ().SpendGamePoint (shopCost.cost);

		PlayerPrefs.SetInt ("PlayerShip_Index", 2);
	}
	public void OnClickBuyButton7()
	{
		ShopCost shopCost = GameObject.Find ("ShipButton3").GetComponentInChildren<ShopCost> ();
		GameObject.Find ("GamePointPanel").GetComponentInChildren<GamePoint> ().SpendGamePoint (shopCost.cost);

		PlayerPrefs.SetInt ("PlayerShip_Index", 3);
	}
	public void OnClickBuyButton8()
	{
		ShopCost shopCost = GameObject.Find ("ShipButton4").GetComponentInChildren<ShopCost> ();
		GameObject.Find ("GamePointPanel").GetComponentInChildren<GamePoint> ().SpendGamePoint (shopCost.cost);

		PlayerPrefs.SetInt ("PlayerShip_Index", 4);
	}

	public void OnClickMainMenuButton()
	{
		//ShopCost shopCost = GameObject.Find ("BuyButton1").GetComponentInChildren<ShopCost> ();
		//GameObject.Find ("GamePointPanel").GetComponentInChildren<GamePoint> ().SpendGamePoint (shopCost.cost);

		SceneManager.LoadScene ("MainMenuScene");
	}
}
