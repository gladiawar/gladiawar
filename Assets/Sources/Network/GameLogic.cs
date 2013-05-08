using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{
	public Transform lambdaPlayerPrefab;
	
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{	
	}
	
	// Network callbacks
	void OnPlayerConnected (NetworkPlayer player)
	{
		Debug.Log ("Server : Player connected (" + player.ToString() + ")");
	}

	void OnServerInitialized ()
	{
		Debug.Log ("Server created");
	}

	void OnConnectedToServer ()
	{
		GetComponent<ShowPlayerLife>().enabled = true;
		Debug.Log ("Client : Connected to server");
	}

	void OnPlayerDisconnected (NetworkPlayer player)
	{
		Debug.Log ("Server : Player disconnected (" + player.ToString() + ")");	
		Network.RemoveRPCs (player);
	}

	void OnDisconnectedFromServer (NetworkDisconnection info)
	{
		Debug.Log ("Client : Disconnected from server (" + info.ToString() + ")");
		Application.LoadLevel("ListePartie");
	}

	void OnFailedToConnect (NetworkConnectionError error)
	{
		Debug.Log ("Client : Failed connection to server (" + error.ToString() + ")");
	}
}
