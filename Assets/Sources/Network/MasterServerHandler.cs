using UnityEngine;
using System.Collections;

public class MasterServerHandler : MonoBehaviour 
{
	/*
	 * Au cas ou l'on voudrait changer les paramètres de configuration 
	 */
	public string	ipMasterServer = "10.224.9.214";
	public int		gamePort = 25000;
	public int		maxPlayers = 32;
	
	/*
	 * Type de la partie en fait : Ne liste que les parties de ce type. 
	 */
	public string	gameName = "GladiaWarArena";
	
	/*
	 * Aura besoin d'un bouton plus tard, peut être. 
	 */
	public bool		useNAT = true;
	
	private string	serverName = "GWA_defaultName";
	private string	serverPasswd = "GWA_defaultPasswd"; // ?
	private string	serverIp = "127.0.0.1";
	private HostData[]	hostList;
	
	// Use this for initialization
	void	Start () 
	{
		MasterServer.ipAddress = ipMasterServer;
		MasterServer.port = 23466;
		MasterServer.ClearHostList();
		MasterServer.RequestHostList(gameName);
	}
	
	private bool	IsConnected()
	{
		if (Network.peerType == NetworkPeerType.Disconnected)
			return (false);
		else
			return (true);
	}
	
	/*
	 * Fonctions liées à une interface
	 */
	void	OnSubmitIp(string ip)
	{
		serverIp = ip;
	}
	
	void	OnSubmitName(string name)
	{
		serverName = name;
	}
	
	void	OnSubmitPwd(string pwd)
	{
		serverPasswd = pwd;
	}
	
	void	OnClickConnect()
	{
	}
	
	void	OnClickSearchForGame()
	{
		MasterServer.RequestHostList(gameName);
		hostList = MasterServer.PollHostList();
		
		if (hostList.Length > 0)
		{
			foreach (HostData hd in hostList)
			{
				//TODO : afficher ça grâce à NGUI
				Debug.Log("Game name : " + hd.gameName);
			}
		}
		else
			//TODO : afficher ça grâce à NGUI
			Debug.Log("No game");
	}
	
	// Ne fonctionne pas correctement !
	void	OnClickStartServer()
	{
		if (serverPasswd.Length > 0)
			Network.incomingPassword = serverPasswd;
		
		Network.InitializeServer(maxPlayers, gamePort, useNAT);
		MasterServer.RegisterHost(gameName, serverName);
		Application.LoadLevel("Multi");
		// De l'autre coté, gérer le bordel avec "OnLevelWasLoaded"
	}
}
