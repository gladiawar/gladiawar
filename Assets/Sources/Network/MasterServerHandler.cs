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
	
	public GameObject	PanelServerList;
	
	// Use this for initialization
	void	Start () 
	{		
		MasterServer.ipAddress = ipMasterServer;
		MasterServer.port = 23466;
		MasterServer.ClearHostList();
		MasterServer.RequestHostList(gameName);
		
		PanelServerList.SetActive(false);
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
	
	void	ConnectToServer(string ip, int port)
	{
		Network.Connect(ip, port);
		// Todo
	}
	
	void	OnClickConnect()
	{
		ConnectToServer(serverIp, gamePort);
	}
	
	private void SearchGames()
	{
		MasterServer.RequestHostList(gameName);
		hostList = MasterServer.PollHostList();
	}
	
	void	OnClickSearchForGame()
	{
		PanelServerList.SetActive(true);
		UITextList	printZone = PanelServerList.transform.Find("Label").GetComponent<UITextList>();
		SearchGames();
		
		printZone.Clear();
		foreach (HostData hd in hostList)
		{
			string	text;
			
			text = "[00FF00]" + hd.gameName + "[-] ";
			text += hd.connectedPlayers + "/" + hd.playerLimit;
			text += " [";
			foreach (string c in hd.ip)
				text += c;
			text += "]";
			
			// Missing : Le bouton connect
			printZone.Add(text);
		}
	}
	
	// Ne fonctionne pas correctement !
	void	OnClickStartServer()
	{
		if (serverPasswd.Length > 0)
			Network.incomingPassword = serverPasswd;
		
		if (Network.InitializeServer(maxPlayers, gamePort, useNAT) == NetworkConnectionError.NoError)
		{
			MasterServer.RegisterHost(gameName, serverName);
			Application.LoadLevel("Multi");
			// De l'autre coté, gérer le bordel avec "OnLevelWasLoaded"
		}
		else
		{
			Debug.LogError("Impossible de creer le server");
			// Gérer l'affichage des erreurs
		}
	}
}
