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
	private string	serverPasswd = "";
	private string	serverIp = "127.0.0.1";
	private HostData[]	hostList;
	
	// Pour gérer le second panel
	public 	GameObject	PanelServerList;
	public	GameObject	PanelServerList_reference;
	
	// Use this for initialization
	void	Start () 
	{		
		MasterServer.ipAddress = ipMasterServer;
		MasterServer.port = 23466;
		MasterServer.ClearHostList();
		MasterServer.RequestHostList(gameName);

		//PanelServerList.SetActive(false);
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
	
	// Todo
	void	ConnectToServer(string ip, int port)
	{
		NetworkConnectionError	value = Network.Connect(ip, port);
		if (value == NetworkConnectionError.NoError)
		{
			Debug.Log("Erreur : " + value.ToString());
			LoadLevel();
			// Ne marche pas
		}
		else
		{
			Debug.LogError("Error while Connecting to server : " + ip + "/" + gamePort);
		}
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
	
	private UILabel GetNewUILabel(GameObject Parent)
	{
		Transform	refT = PanelServerList_reference.transform;
		GameObject 	go = (GameObject)UILabel.Instantiate(	PanelServerList_reference, 
															new Vector3(refT.position.x, refT.position.y, refT.position.z), 
															new Quaternion(0, 0, 0, 0));
		go.transform.parent = Parent.transform;
		go.transform.localScale = refT.localScale;
		go.SetActive(true);
		return (go.GetComponent<UILabel>());
	}
	
	private void DeleteChildren(string name)
	{
		Transform tf;
		
		while ((tf = PanelServerList.transform.FindChild(name)) != null)
		{
			tf.parent = null;
			Destroy(tf.gameObject);
		}
	}
	
	void	OnClickSearchForGame()
	{
		PanelServerList.SetActive(true);
		SearchGames();
		DeleteChildren("LabelGame");

		int nbServer = 0;
		foreach (HostData hd in hostList)
		{
			UILabel label = this.GetNewUILabel(PanelServerList);
			label.transform.position -= new Vector3(0, (float)nbServer / 16f, 0);	// Allez savoir pourquoi 16 ....
			label.name = "LabelGame";
			string	text = "[00FF00]" + hd.gameName + "[-] " + hd.connectedPlayers + "/" + hd.playerLimit + " [";
//			foreach (string c in hd.ip) // Plusieurs addresses (valides ?)
//			{
				text += hd.ip.GetValue(0);
//			}
			text += "]";
			label.text = text;

			nbServer++;
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
			LoadLevel();
			// De l'autre coté, gérer le bordel avec "OnLevelWasLoaded"
		}
		else
		{
			Debug.LogError("Impossible de creer le server");
			// Gérer l'affichage des erreurs
		}
	}
	
	private	void LoadLevel()
	{
		Application.LoadLevel("Multi");
	}
}
