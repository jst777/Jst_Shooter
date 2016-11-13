using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizMo : MonoBehaviour {
	public Color _color = Color.yellow;
	public float _radius = 0.1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnDrawGizMos(){
		Gizmos.color = _color;
		Gizmos.DrawWireSphere(transform.position, _radius);
		Debug.Log ("OnDrawGizMos");
	}
}
