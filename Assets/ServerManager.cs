using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

using System;

public class ServerManager : MonoBehaviour {
	SocketIOComponent socket;
	// Use this for initialization
	void Start () {
		socket = Camera.main.GetComponent<SocketIOComponent> ();

		socket.On ("chat", OnChat);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnChat(SocketIOEvent e){
		JSONObject obj = e.data;
		string name = obj.GetField ("user").str;
		string message = obj.GetField ("msg").str;
		string date = obj.GetField ("date").str;
		MainMenuUI mainmenu = Camera.main.GetComponent<MainMenuUI> ();
		mainmenu.AddChatText (name, message, date);
	}

	public void EmitChat(string name, string msg, DateTime date)
	{
		JSONObject obj = new JSONObject (JSONObject.Type.OBJECT);
		obj.AddField ("user", name);
		obj.AddField ("msg", msg);
		obj.AddField ("date", date.ToString ());

		socket.Emit ("chat", obj);
	}
}
