using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{
	public string ip;
	public int port;
	public bool useNat;
	public int maxPlayers;
	public Transform lambdaPlayerPrefab;
	private bool activeConnection = false;
	
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{	
	}
	
	void OnGUI ()
	{
		if (!activeConnection)
		{
			if (GUI.Button(new Rect (10, 10, 200, 30), "Create : " + port))
			{
				if (Network.InitializeServer (maxPlayers, port, useNat) != NetworkConnectionError.NoError) 
					Debug.LogError ("Unable to create server");
			}
			else if (GUI.Button(new Rect (10, 50, 200, 30), "Connect : " + ip + ";" + port))
			{
				if (Network.Connect(ip, port) != NetworkConnectionError.NoError)
					Debug.LogError ("Unable to connect to server");
			}
		}
		else
		{
			if (GUI.Button (new Rect (10, 10, 200, 30), "Disconnect"))
				Network.Disconnect ();
		}
	}
	
	// Network callbacks
	void OnPlayerConnected (NetworkPlayer player)
	{
		Debug.Log ("Server : Player connected");
	}

	void OnServerInitialized ()
	{
		Debug.Log ("Server created");
		activeConnection = true;
	}

	void OnConnectedToServer ()
	{
		GetComponent<ShowPlayerLife>().enabled = true;
		Debug.Log ("Client : Connected to server");
		activeConnection = true;
		Network.Instantiate (lambdaPlayerPrefab, new Vector3 (0, 1, 0), new Quaternion (0, 0, 0, 0), 0);
	}

	void OnPlayerDisconnected (NetworkPlayer player)
	{
		Debug.Log ("Server : Player disconnected");	
		Network.RemoveRPCs (player);
		Network.DestroyPlayerObjects (player);
	}

	void OnDisconnectedFromServer (NetworkDisconnection info)
	{
		Debug.Log ("Client : Disconnected from server");
		activeConnection = false;
		Application.LoadLevel(Application.loadedLevel);
	}

	void OnFailedToConnect (NetworkConnectionError error)
	{
		Debug.Log ("Client : Failed connection to server");
		activeConnection = false;
	}
}
