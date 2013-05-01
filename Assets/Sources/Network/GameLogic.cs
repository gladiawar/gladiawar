using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{
	public string ip;
	public int port;
	public bool useNat;
	public int maxPlayers;
	public Transform lambdaPlayerPrefab;
	
	// Use this for initialization
	void Start ()
	{
		Network.Instantiate (lambdaPlayerPrefab, new Vector3 (0, 1, 0), new Quaternion (0, 0, 0, 0), 0);
	}
	
	// Update is called once per frame
	void Update ()
	{	
	}
	
	// Network callbacks
	void OnPlayerConnected (NetworkPlayer player)
	{
		Debug.Log ("Server : Player connected");
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
		Debug.Log ("Server : Player disconnected");	
		Network.RemoveRPCs (player);
		Network.DestroyPlayerObjects (player);
	}

	void OnDisconnectedFromServer (NetworkDisconnection info)
	{
		Debug.Log ("Client : Disconnected from server");
		Application.LoadLevel(Application.loadedLevel);
	}

	void OnFailedToConnect (NetworkConnectionError error)
	{
		Debug.Log ("Client : Failed connection to server");
	}
}
