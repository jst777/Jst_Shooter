﻿using System.Collections;
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
		PlayerPrefs.SetInt ("Fellow1", 1);
	}
	public void OnClickBuyButton2()
	{
		PlayerPrefs.SetInt ("Fellow2", 1);
	}
	public void OnClickBuyButton3()
	{
		PlayerPrefs.SetInt ("Fellow3", 1);
	}

	public void OnClickBuyButton4()
	{
		PlayerPrefs.SetInt ("Fellow4", 1);
	}

	public void OnClickMainMenuButton()
	{
		SceneManager.LoadScene ("MainMenuScene");
	}
}