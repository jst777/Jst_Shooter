using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour {
	private Transform tr;
	private LineRenderer line;

	private RaycastHit hit;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();
		line = GetComponent<LineRenderer> ();

		line.useWorldSpace = false;
		line.enabled = false;
		float laserWidth = 3.0f;
		line.startWidth = laserWidth;
		line.endWidth = laserWidth;
		//line.endWidth = 0.01f;
	}
	
	// Update is called once per frame
	void Update () {
		if (line.enabled) {
			Ray ray = new Ray (tr.position + (Vector3.up * 0.02f), tr.forward);
			Debug.DrawRay (ray.origin, ray.direction * 100, Color.green);
			line.SetPosition (0, tr.InverseTransformPoint (ray.origin));
			line.SetPosition (1, tr.InverseTransformPoint (ray.GetPoint (100.0f)));

			if (Physics.Raycast (ray, out hit, 100.0f)) {
				EnemyScript enemyScript = hit.collider.gameObject.GetComponent<EnemyScript> ();
				if (enemyScript != null) {
					Debug.Log ("LaserBeam - broke" + hit.collider.gameObject.name);
					enemyScript.OnDamaged ();
				}
				else {
					Destroy (hit.collider.gameObject);
				}
			}
		}
	}

	public void FireLaserBeam()
	{
		Ray ray = new Ray (tr.position + (Vector3.up * 0.02f), tr.forward);
		Debug.DrawRay (ray.origin, ray.direction * 100, Color.green);
		line.SetPosition (0, tr.InverseTransformPoint (ray.origin));
		line.SetPosition (1, tr.InverseTransformPoint (ray.GetPoint (100.0f)));

		if (Physics.Raycast (ray, out hit, 100.0f)) {
			EnemyScript enemyScript = hit.collider.gameObject.GetComponent<EnemyScript> ();
			if (enemyScript != null) {
				Debug.Log ("LaserBeam - broke" + hit.collider.gameObject.name);
				enemyScript.OnDamaged ();
			} else {
				Destroy (hit.collider.gameObject);
			}
		}

		StartCoroutine (this.ShowLaserBeam ());
	}

	public bool IsLaserVisible()
	{
		return line.enabled;
	}

	IEnumerator ShowLaserBeam(){
		line.enabled = true;
		yield return new WaitForSeconds (1.0f);//Random.Range (0.01f, 0.2f));
		line.enabled = false;
	}
}
