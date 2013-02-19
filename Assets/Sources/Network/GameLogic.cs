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
	private GUIStyle guiStyle;
	
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
		guiStyle = GUI.skin.label;
		guiStyle.alignment = TextAnchor.MiddleCenter;
		
		GUILayout.BeginVertical(GUILayout.Width(200));
		if (!activeConnection)
		{
			if (GUILayout.Button("Create : " + port, GUILayout.Height(34)))
			{
				if (Network.InitializeServer (maxPlayers, port, useNat) != NetworkConnectionError.NoError) 
					Debug.LogError ("Unable to create server");
			}
			if (GUILayout.Button("Connect to " + ip + ":" + port, GUILayout.Height(34)))
			{
				if (Network.Connect(ip, port) != NetworkConnectionError.NoError)
					Debug.LogError ("Unable to connect to server");
			}
			GUILayout.BeginHorizontal(GUILayout.Height(34));
			GUILayout.Label(" Adresse : ", guiStyle);
			ip = GUILayout.TextField(ip, guiStyle);
			GUILayout.EndHorizontal();
		}
		else
		{
			GUILayout.BeginVertical(GUILayout.Width(200));
			if (GUILayout.Button ("Disconnect", GUILayout.Height(34)))
				Network.Disconnect ();
			GUILayout.EndVertical();
		}
		GUILayout.EndVertical();
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
