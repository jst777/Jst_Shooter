using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCost : MonoBehaviour {
	public int cost = 0;

	// Use this for initialization
	void Start () {
		GetComponentInChildren<Text> ().text = "GP : " + cost.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
