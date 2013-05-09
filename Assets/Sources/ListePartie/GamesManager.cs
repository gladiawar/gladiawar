using UnityEngine;
using System.Collections;

public class GamesManager : MonoBehaviour 
{
	// Pour gérer le second panel
	public 	GameObject			PanelServerList;

	// Pour cloner un Button (de référence) facilement
	public	GameObject			UIButtonToClone;
	
	// Le chat du bas, pour afficher des messages facilement
	public	UITextList			MessageBox;
	
	// Le label qui affiche le nom du personnage
	public	UILabel				labelPlayerName;
	
	// Les données de cette scène
	private	GamesManagerData	data;
		
	// Un compteur interne pour savoir quelle adresse est utilisée
	private int MasterServerIpCounter = 0;
	
	void	Awake ()
	{
		data = GetComponent<GamesManagerData>();
	}
	
	void	Start () 
	{	
		MasterServer.ipAddress = data.masterServerIp[MasterServerIpCounter];
		MasterServer.port = data.masterServerPort;
		MasterServer.ClearHostList();
		MasterServer.RequestHostList(data.gameType);
		
		GameObject	playerinfo = GameObject.Find("PlayerPrefs");
		if (playerinfo)
			labelPlayerName.text += playerinfo.GetComponent<PlayerInfo>().GetPlayerName();
	}
	
	// Bouton
	void	OnClickConnectGame()
	{
		// Connect retourne tout de suite (Généralement pas d'erreur), et
		// envoie la réponse plus tard, sous forme de (Unity)messages.
		Network.Disconnect();
		NetworkConnectionError netError = Network.Connect(data.gameIp, data.gamePort, data.gamePassword);

		if (netError != NetworkConnectionError.NoError)
		{
			LogError("Connection Error : " + netError.ToString());
		}
		else
		{
			Log("Tentative de connection au server : " + data.gameIp[0] + "/" + data.gamePort + " ...", "FFFF00");
		}

	}
	
	// Message from Network
	void 	OnFailedToConnect(NetworkConnectionError error)
	{
		LogError("Impossible de se connecter au serveur " + data.gameIp[0] + " ; " + error.ToString());
	}
	
	void	OnServerInitialized()
	{
		MasterServer.RegisterHost(data.gameType, data.gameName);
		GameObject.Find("LevelChanger").GetComponent<LevelChanger>().LoadLevel("Game");
	}
	
	// Message from MasterServer
	void	OnMasterServerEvent(MasterServerEvent MSevent)
	{
		switch (MSevent)
		{
		case MasterServerEvent.HostListReceived:
			this.RefreshGamesList();
			break;
			
		case MasterServerEvent.RegistrationSucceeded:
			break;
			
		case MasterServerEvent.RegistrationFailedGameName:
			this.LogError(MSevent.ToString());
			break;
			
		case MasterServerEvent.RegistrationFailedGameType:
			this.LogError(MSevent.ToString());
			break;
			
		case MasterServerEvent.RegistrationFailedNoServer:
			this.LogError(MSevent.ToString());
			break;
			
		default:
			this.LogWarning(MSevent.ToString());
			break;
		}
	}

	// Message from MasterServer
	void	OnFailedToConnectToMasterServer(NetworkConnectionError error)
	{
		LogError("Le MasterServer ne repond pas. Listage des parties impossible : " + error.ToString());
		
		/*
		 * BUG: Masterserver n'est plus op après une erreur
		if (MasterServerIpCounter > data.masterServerIp.Length)
			LogError("Le MasterServer ne repond pas sur aucune adresse. Listage des parties impossible : " + error.ToString());
		else
		{
			string newIP = data.masterServerIp[MasterServerIpCounter];
			
			LogWarning("Impossible de joindre le MasterServer sur " + MasterServer.ipAddress + " : Nouvelle tentative sur " + newIP);
			MasterServer.ipAddress = newIP;
			++MasterServerIpCounter;
			MasterServer.ClearHostList();
			MasterServer.RequestHostList(data.gameType);
		}
		*/
	}
	
	// Utilisé par RefreshGamesList
	private void DeletePanelserverChildren(string name)
	{
		Transform tf;
		
		while ((tf = PanelServerList.transform.FindChild(name)) != null)
		{
			tf.parent = null;
			Destroy(tf.gameObject);
		}
	}
	
	// Utilisé par RefreshGamesList
	private UILabel GetNewUIButtonLabel()
	{
		Transform	refT = UIButtonToClone.transform;
		GameObject 	go = (GameObject)UILabel.Instantiate(	UIButtonToClone, 
															new Vector3(refT.position.x, refT.position.y, refT.position.z), 
															new Quaternion(0, 0, 0, 0));
		go.transform.parent = PanelServerList.transform;
		go.transform.localScale = refT.localScale;
		go.name = "LabelGame";
		go.SetActive(true);
		return (go.GetComponentInChildren<UILabel>());
	}
	
	// Appelé lors le MasterServer a renvoyer la liste d'hôte
	private void RefreshGamesList()
	{
		DeletePanelserverChildren("LabelGame");
		PanelServerList.transform.FindChild("LabelLastUpdate").GetComponent<UILabel>().text = "Last update : " + (System.DateTime.Now);
		
		int nbServer = 0;
		HostData[] hostList = MasterServer.PollHostList();
		foreach (HostData hd in hostList)
		{
			UILabel label = this.GetNewUIButtonLabel();
			//TODO: Fix this dark magic avec NGUITools (voir ReadyList.cs)
			label.transform.parent.position -= new Vector3(0, (float)nbServer / 14f, 0);	// Allez savoir pourquoi y'a un facteur 400 ....
			label.depth += nbServer;
			string	text = "[00FF00]" + hd.gameName + "[-] " + hd.connectedPlayers + "/" + hd.playerLimit + " [";
			//TODO: Récupérer les autres infos
			//TODO: Vérifier si ça marche VRAIMENT
			text += hd.ip[0] + "]";
			label.transform.parent.GetComponent<OnClickJoinGame>().gameIp = hd.ip;
			label.text = text;
			nbServer++;
		}
		
		if (nbServer == 0)
		{
			string[] defaultValue = {"127.0.0.1"};
			
			UILabel label = this.GetNewUIButtonLabel();
			label.transform.parent.GetComponent<OnClickJoinGame>().gameIp = defaultValue;
			label.text = "No one like you ...";
			label.color = Color.cyan;
		}
		Log("Liste des parties mise a jour.");
	}
	
	// Message : Bouton
	void	OnClickSearchForGame()
	{
		MasterServer.ClearHostList();
		MasterServer.RequestHostList(data.gameType);
		Log("Recherche des parties existantes ...", "FFFF00");
	}
	
	// Message : Bouton
	void	OnClickStartServer()
	{
		if (data.gamePassword.Length > 0)
			Network.incomingPassword = data.gamePassword;
		if (data.gameName.Length == 0)
		{
			LogWarning("Impossible de creer une partie sans nom.");
			return;
		}
		
		if (Network.InitializeServer(data.gameMaxPlayers, data.gamePort, data.useNat) != NetworkConnectionError.NoError)
		{
			LogError("Impossible de creer une nouvelle partie.");
		}
	}
	
	// Affiche un message en rouge dans le chat du bas + Debug.logerreur
	private void LogError(string msg)
	{
		MessageBox.Add("[FF3838]" + msg + "[-]"); // Rouge
		Debug.LogError(msg);
	}
	
	// Affiche un message en orange dans le chat du bas + Debut.log
	private void LogWarning(string msg)
	{
		MessageBox.Add("[E17417]" + msg + "[-]"); // Orange
		Debug.Log(msg);
	}
	
	// Affiche un message en vert dans le chat du bas
	private void Log(string msg)
	{
		MessageBox.Add("[44F058]" + msg + "[-]"); // Vert
	}
	
	// Affiche un message de la couleur choisie dans le chat du bas
	private void Log(string msg, string ColorText)
	{
		MessageBox.Add("[" + ColorText + "]" + msg + "[-]");
	}
}
