using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour {
	bool visible = false;
	// Use this for initialization
	void Start () {
		gameObject.transform.localScale = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Escape)) {
			if (visible) {
				gameObject.transform.localScale = new Vector3 (0, 0, 0);
				visible = false;
			} else {
				gameObject.transform.localScale = new Vector3 (1, 1, 1);
				visible = true;
			}
		}
	}

	public void OnClickExitButton()
	{
		//Debug.Log ("Exit");
		Application.Quit ();
	}
}
