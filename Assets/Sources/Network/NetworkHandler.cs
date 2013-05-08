using UnityEngine;
using System.Collections;

public class NetworkHandler : MonoBehaviour 
{
	private string	LoaderLevel = "ListePartie";
	
	// Called before Start()
	void Awake()
	{
		DontDestroyOnLoad(this);
		networkView.group = 1;
	}
	
	// Message Network
	void OnServerInitialized ()
	{
		Network.RemoveRPCsInGroup(0);
		Network.RemoveRPCsInGroup(1);
		networkView.RPC("NetworkLoadLevel", RPCMode.AllBuffered, Application.loadedLevel + 1);
	}
	
	// Message Network
	void OnDisconnectedFromServer (NetworkDisconnection mode)	
	{
		Network.Disconnect();
	}
	
	[RPC]
	public void	NetworkLoadLevel(int levelNum)
	{
		Network.SetSendingEnabled(0, false);
		Network.isMessageQueueRunning = false;
		
		//Network.SetLevelPrefix(levelNum);
		Application.LoadLevel(levelNum);
		
		Network.isMessageQueueRunning = true;
		Network.SetSendingEnabled(0, true);
	}
}
