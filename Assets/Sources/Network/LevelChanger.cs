using UnityEngine;
using System.Collections;

public class LevelChanger : MonoBehaviour 
{
	public static LevelChanger	levelChanger = null;
	
	void	Awake()
	{
		if (LevelChanger.levelChanger == null)
			LevelChanger.levelChanger = this;
	}
	
	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad(this);
		networkView.group = 1;
	}

	// Vire les doublons (de l'éditeur), pour éviter de se taper l'écran de login
	void	OnLevelWasLoaded()
	{
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("LevelChanger"))
		{
			if (go != LevelChanger.levelChanger)
				GameObject.Destroy(go);
		}
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
		
		foreach (Object obj in FindObjectsOfType(typeof(GameObject)))
		{
			GameObject go;
			
			if ((go = (GameObject)(obj)) != null)
				go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
		}
	}
	
	void	OnDisconnectedFromServer(NetworkDisconnection info)
	{
		Debug.Log (info.ToString());
		Application.LoadLevel("ListePartie");
	}
}

