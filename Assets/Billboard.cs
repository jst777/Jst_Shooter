using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {
	public Transform Target;

	// Use this for initialization
	void Start () {
		Target = Camera.main.gameObject.transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.rotation = Quaternion.LookRotation (Target.forward);
	}
}
