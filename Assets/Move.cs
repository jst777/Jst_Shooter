using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Move : MonoBehaviour {
	Vector3 lookDir;
	public float moveSpeed;
	private Rigidbody m_Rigidbody;
	private Vector3 camForward; // The current forward direction of the camera
	private Transform cam; // A reference to the main camera in the scenes transform
	// Use this for initialization
	void Start () {
		m_Rigidbody = GetComponent<Rigidbody> ();
	}

	private void Awake()
	{
		if (Camera.main != null)
		{
			cam = Camera.main.transform;
		}
	}


	public void MoveToDirection(Vector3 moveDirection)//, bool jump)
	{
		m_Rigidbody.AddForce(moveDirection*moveSpeed);
		// If using torque to rotate the ball...

		/*if (m_UseTorque)
		{
			// ... add torque around the axis defined by the move direction.
			m_Rigidbody.AddTorque(new Vector3(moveDirection.z, 0, -moveDirection.x)*m_MovePower);
		}
		else
		{
			// Otherwise add force in the move direction.
			m_Rigidbody.AddForce(moveDirection*m_MovePower);
		}

		// If on the ground and jump is pressed...
		if (Physics.Raycast(transform.position, -Vector3.up, k_GroundRayLength) && jump)
		{
			// ... add force in upwards.
			m_Rigidbody.AddForce(Vector3.up*m_JumpPower, ForceMode.Impulse);
		}*/
	}
	// Update is called once per frame
	void Update () {
		// 현재 터치되어 있는 카운트 가져오기
		int cnt = Input.touchCount;

		// 동시에 여러곳을 터치 할 수 있기 때문
		for (int i = 0; i < cnt; ++i) {

			Touch touch = Input.GetTouch (i);

			Vector2 pos = touch.position;

			//gameUI.textGoal.text = pos.ToString ();
			//skip touch event when object selected
			if (EventSystem.current.currentSelectedGameObject != null) {
				return;
			}

			Vector3 p = Camera.main.ScreenToWorldPoint (new Vector3 (pos.x, pos.y, 30));//Camera.main.nearClipPlane));
			p.y = 0.0f;//1.6f;
			transform.position = Vector3.Lerp (transform.position, p, Time.deltaTime * 1.2f);

			// Todo later 
			//Vector3 movement = p - transform.position;//new Vector3 (p.x, 0, p.z);//;;moveHorizontal, 0.0f, moveVertical);
			//GetComponentInChildren<Rigidbody>().velocity = movement.normalized * moveSpeed;
			//GetComponentInChildren<Rigidbody> ().rotation = Quaternion.Euler (0.0f, 0.0f, movement.x * -5);//GetComponent<Rigidbody>().velocity.x * -5);
		}

		//UIMgr uiMgr = GetComponent<UIMgr> ();
		if (Input.GetKey (KeyCode.LeftArrow) ||
			Input.GetKey (KeyCode.RightArrow) ||
			Input.GetKey (KeyCode.UpArrow) ||
			Input.GetKey (KeyCode.DownArrow)) {
			float xx = Input.GetAxisRaw ("Vertical");
			float zz = Input.GetAxisRaw ("Horizontal");

			lookDir = transform.forward * xx + zz * transform.right;//xx * Vector3.forward + zz * Vector3.right;
			//MoveToDirection (lookDir.normalized);
			/*
			if (cam != null) {
				// calculate camera relative direction to move:
				camForward = Vector3.Scale (cam.forward, new Vector3 (1, 0, 1)).normalized;
				Vector3 vPos = transform.position;// + Vector3 (0, 3, 0);
				//Camera.main.transform.Translate (vPos);
				Debug.Log (Camera.main.transform.position.ToString ());
				lookDir = transform.up;
				//Camera.main.transform.rotation = Quaternion.LookRotation (lookDir);
			}


			*/
			this.transform.position += (lookDir * moveSpeed * Time.deltaTime);

			//float moveHorizontal = Input.GetAxis ("Horizontal");
			//float moveVertical = Input.GetAxis ("Vertical");
			//Vector3 movement = new Vector3 (zz, 0, xx);//;;moveHorizontal, 0.0f, moveVertical);
			//GetComponentInChildren<Rigidbody>().velocity = movement * moveSpeed;
			//GetComponentInChildren<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -5);
		}
		if (Input.GetKey (KeyCode.Q)) {
			transform.Rotate (-1 * Vector3.up * Time.deltaTime * 100, Space.World);
		} else if (Input.GetKey (KeyCode.E)) {
			transform.Rotate(Vector3.up * Time.deltaTime * 100, Space.World);
		}
	}

}
