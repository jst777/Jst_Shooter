using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFormation : MonoBehaviour {
	public float offsetXZ = 1.0f;
	public enum eFormation
	{
		ePyramidFormation,
		eArchFormation,
		eHakikFormation,
		eCarrierFormation,
		eMaxFormation
	}
	public enum eCarrierAction
	{
		eCarrierSeperation,
		eCarrierDashToPlayer,
		//eCarrierReturn,
		//eCarrierAlignment,
		eCarrierCohesion,
		eCarrierActionMax,
	}
		
	public eFormation formation = eFormation.ePyramidFormation;
	public eCarrierAction carrierAction = eCarrierAction.eCarrierSeperation;

	public bool spearationAngleSet = false;

	// Use this for initialization
	void Start () {
		CalculateOffsetPos ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CalculateOffsetPos()
	{
		string[] splitString = name.Split(new string[] { "_", "\n"}, System.StringSplitOptions.None);

		if (splitString.Length >= 2 && GetComponent<EnemyScript>().isSetPos == false) {
			int idx = 0;
			//Debug.Log ("splitString.length" + splitString.Length.ToString() + splitString[1].ToString());		
			if (int.TryParse (splitString [1], out idx)) {
			}

			if (idx != 0) {
				switch (formation) {
				case eFormation.ePyramidFormation:
					CalculatePyramidFormation (idx);
					break;
				case eFormation.eHakikFormation:
					CalculateHakikFormation (idx);
					break;
				case eFormation.eArchFormation:
					CalculateArchFormation (idx);
					break;
				}
			} else {
				//tag = "BOSS"; //set index 0 to boss
			}

			EnemyScript enemyScript = GetComponent<EnemyScript> ();
			if (enemyScript != null) {
				if (enemyScript.destpoints.Length > enemyScript.destinationIndex) {
					enemyScript.vDestination = enemyScript.destpoints [enemyScript.destinationIndex].transform.position + enemyScript.vOffsetToBoss;
				}
			}
		}
	}

	void CalculatePyramidFormation(int index)
	{
		int sum = 0;
		int selectedCol = 0;
		offsetXZ = 2.0f;

		for (int col = 1; col < 100; col++) {
			sum += col;
			if (index < sum) {
				selectedCol = col;
				break;	
			}
		}

		sum -= selectedCol;
		selectedCol -= 1;


		float centerIndex = sum + (selectedCol) * 0.5f;
		float pos = index - centerIndex;

		//Debug.Log ("[x][z] = " + pos.ToString () + " " + centerIndex.ToString () + " " + selectedCol.ToString ());
		//Debug.Log ("offsetXZ = " + offsetXZ.ToString());

		GetComponent<EnemyScript>().vOffsetToBoss = new Vector3 (offsetXZ * pos, 0, offsetXZ * selectedCol);
	}
	void CalculateHakikFormation(int index){
		Vector3 vInitPos = new Vector3 (0, 0, -1);
		vInitPos *= offsetXZ;

		int maxMonster = Camera.main.GetComponent<GameMgr> ().maxMonster - 1;


		float fPartitionTheta = 180 / maxMonster;
		float rad_step = Mathf.PI / 180 * fPartitionTheta;

		//y^2 = 4px


		// -1 0 1 2 3


		int halfmaxMonster = maxMonster / 2;
		rad_step *= (index - halfmaxMonster);

		//float c = Mathf.Cos(rad_step);
		//float s = Mathf.Sin (rad_step);
		Vector3 pos;
		/*
		pos.x = vInitPos.x * c - vInitPos.z * s;
		pos.z = vInitPos.x * s + vInitPos.z * c;
		*/
		pos.x = (index-1 - halfmaxMonster) * 2;
		//Debug.Log ("posX =" + pos.x.ToString ());
		float fgradient = 1 / (float)Mathf.Pow(halfmaxMonster, 1.2f);//0.2f;
		pos.z = fgradient * -1 * Mathf.Pow (Mathf.Abs(pos.x), 2f);
		//pos.z = 2 * Mathf.Sqrt (Mathf.Abs (pos.x));

		pos.y = 0;

		GetComponent<EnemyScript> ().vOffsetToBoss = pos;
	}
	void CalculateArchFormation(int index)
	{
		int maxMonster = Camera.main.GetComponent<GameMgr> ().maxMonster - 1;


		float fPartitionTheta = 180 / maxMonster;
		float rad_step = Mathf.PI / 180 * fPartitionTheta;
		offsetXZ = 5;

		int halfmaxMonster = maxMonster / 2;
		rad_step *= (index - halfmaxMonster);

		//Debug.Log ("rad_Step = " + rad_step);
		
		float c = Mathf.Cos(rad_step);
		float s = Mathf.Sin (rad_step);

		//velocity.x = fireTransform.forward.x * c - fireTransform.forward.z * s;
		//velocity.z = fireTransform.forward.x * s + fireTransform.forward.z * c;
		Vector3 pos;
		Vector3 forward = new Vector3 (0, 0, -1);
		pos.x = forward.x * c - forward.z * s;
		pos.z = forward.x * s + forward.z * c;
		pos.y = 0;
		pos = pos.normalized * offsetXZ;
		//pos *= offsetXZ;

		GetComponent<EnemyScript> ().vOffsetToBoss = pos;//new Vector3 (offsetXZ * pos.x, 0, offsetXZ * pos.z);
	}

}
