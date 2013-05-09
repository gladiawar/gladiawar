using UnityEngine;
using System.Collections;

public class LevelChanger : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad(this);
		networkView.group = 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	// Vire les doublons (de l'éditeur), pour éviter de se taper l'écran de login
	void	OnLevelWasLoaded()
	{
		// TODO: Eviter les doublons
	}
	
	public void	LoadLevel(string name)
	{
		Network.RemoveRPCsInGroup(0);
		Network.RemoveRPCsInGroup(1);
		networkView.RPC("RPC_LoadLevel", RPCMode.AllBuffered, "Game");
	}
	
	[RPC]
	void	RPC_LoadLevel(string name)
	{
		Network.SetSendingEnabled(0, false);
		Network.isMessageQueueRunning = false;
		Network.SetLevelPrefix(2);
		Application.LoadLevel(name);
		Network.isMessageQueueRunning = true;
		Network.SetSendingEnabled(0, true);
	}
	
	void	OnDisconnectedFromServer(NetworkDisconnection info)
	{
		Debug.Log (info.ToString());
		Application.LoadLevel("ListePartie");
	}
}

