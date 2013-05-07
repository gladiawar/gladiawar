using UnityEngine;
using System.Collections;

public class NetworkHandler : MonoBehaviour 
{
	// Called before Start()
	void Awake()
	{
		DontDestroyOnLoad(this);
		//networkView.group = 1;
	}
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	// Message Network
	void OnPlayerConnected (NetworkPlayer player)
	{
		Debug.Log("OnPlayerConnected");
	}
	
	// Message Network
	void OnServerInitialized ()
	{
		Debug.Log("OnServerInitialized");
	}
	
	// Message Network
	void OnConnectedToServer ()
	{
		Debug.Log("OnConnectedToServer");
	}
	
	// Message Network
	void OnPlayerDisconnected (NetworkPlayer player)
	{
		Debug.Log("OnPlayerDisconnected");
	}
	
	// Message Network
	void OnDisconnectedFromServer (NetworkDisconnection mode)	
	{
		Debug.Log("OnDisconnectedFromServer");
	}
	
	// Message Network
	void OnFailedToConnect (NetworkConnectionError error)
	{
		Debug.Log("OnFailedToConnect");
	}
	
	// Message Network
	void OnNetworkInstantiate (NetworkMessageInfo info)
	{
		Debug.Log("OnNetworkInstantiate");
	}
}
