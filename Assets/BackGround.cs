using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour {
	private bool setBG = false;
	// Use this for initialization
	void Start () {
		float random = Random.Range (1, 5);
		string textureName = "bg_" + random.ToString ();
		Texture texture =  (Texture)Resources.Load ("Textures/" + textureName);
		//Debug.Log ("BackGround script " + texture.ToString ());
		if (texture != null) {
			GameObject bg = GameObject.Find ("BackGround");
			if (bg != null) {
				MeshRenderer meshRender = GetComponent<MeshRenderer> ();
				//Debug.Log ("BackGround script " + meshRender.ToString());
				if (meshRender != null) {
					if (meshRender.materials.Length > 0) {
						meshRender.materials [0].mainTexture = texture;
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!setBG) {
			/*
			setBG = true;
			float random = Random.Range (1, 5);
			string textureName = "bg_" + random.ToString ();
			Texture texture =  (Texture)Resources.Load ("Textures/" + textureName);
			//Debug.Log ("BackGround script " + texture.ToString ());
			if (texture != null) {
				GameObject bg = GameObject.Find ("BackGround");
				if (bg != null) {
					MeshRenderer meshRender = GetComponent<MeshRenderer> ();
					Debug.Log ("BackGround script " + meshRender.ToString());
					if (meshRender != null) {
						if (meshRender.materials.Length > 0) {
							meshRender.materials [0].mainTexture = texture;
						}
					}
				}
			}
			*/
		}
	}
}
