using UnityEngine;
using System.Collections;

public class GamesManager : MonoBehaviour 
{
	// Les données de cette scène
	public	GamesManagerData	data;
	
	// Pour rafraichir facilement la liste des autres parties
	private HostData[]			hostList;
	
	// Pour gérer le second panel
	public 	GameObject			PanelServerList;
	
	// Pour cloner un Button (de référence) facilement
	public	GameObject			UIButtonToClone;
	
	public	UITextList			MessageBox;
	
	void	Start () 
	{		
		MasterServer.ipAddress = data.masterServerIp;
		MasterServer.port = data.masterServerPort;
		MasterServer.ClearHostList();
		MasterServer.RequestHostList(data.gameType);
	}
	
	void	OnClickConnectGame()
	{
		// Connect retourne tout de suite (Généralement pas d'erreur), et
		// envoie la réponse plus tard, sous forme de (Unity)messages.
		NetworkConnectionError netError = Network.Connect(data.gameIp, data.gamePort, data.gamePassword);
		
		// TODO: Network accept des string[], changer ça puisque l'on peut en récuper dans hd.ip[] (voir doc)
		if (netError != NetworkConnectionError.NoError)
		{
			LogError("Connection Error : " + netError.ToString());
		}
		else
		{
			Log("Tentative de connection au server : " + data.gameIp + "/" + data.gamePort + " ...");
		}

	}
	
	// Message
	void 	OnFailedToConnect(NetworkConnectionError error)
	{
		LogError("Impossible de se connecter au serveur " + data.gameIp + " ; " + error.ToString());
	}
	
	// Message
	// TODO: Le résultat est très drôle si on l'est pas sur la même map. Il faut corriger ça
	void	OnConnectedToServer()
	{
		Log("Connection reussie.");
	}
	
	private void SearchGames()
	{
		MasterServer.RequestHostList(data.gameType);
		hostList = MasterServer.PollHostList();
	}
	
	// TODO: C'est utilise l'argument ?
	private UILabel GetNewUIButtonLabel(GameObject Parent)
	{
		Transform	refT = UIButtonToClone.transform;
		GameObject 	go = (GameObject)UILabel.Instantiate(	UIButtonToClone, 
															new Vector3(refT.position.x, refT.position.y, refT.position.z), 
															new Quaternion(0, 0, 0, 0));
		go.transform.parent = Parent.transform;
		go.transform.localScale = refT.localScale;
		go.name = "LabelGame";
		go.SetActive(true);
		return (go.GetComponentInChildren<UILabel>());
	}
	
	private void DeletePanelserverChildren(string name)
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
		DeletePanelserverChildren("LabelGame");
		PanelServerList.transform.FindChild("LabelLastUpdate").GetComponent<UILabel>().text = "Last update : " + (System.DateTime.Now);
		
		int nbServer = 0;
		foreach (HostData hd in hostList)
		{
			// TODO: Changer le "depth" pour chaque nouveau label
			UILabel label = this.GetNewUIButtonLabel(PanelServerList);
			label.transform.parent.position -= new Vector3(0, (float)nbServer / 14f, 0);	// Allez savoir pourquoi 16 ....
			string	text = "[00FF00]" + hd.gameName + "[-] " + hd.connectedPlayers + "/" + hd.playerLimit + " [";
			// hd est un string[]
			
			string ip = (string)(hd.ip.GetValue(0));
			text += ip + "]";
			label.transform.parent.GetComponent<OnClickJoinGame>().gameIp = ip;
			label.text = text;
			nbServer++;
		}
		
		if (nbServer == 0)
		{
			UILabel label = this.GetNewUIButtonLabel(PanelServerList);
			label.transform.parent.GetComponent<OnClickJoinGame>().gameIp = "127.0.0.1";
			label.text = "No one like you ...";
			label.color = Color.cyan;
		}
	}
	
	// TODO: Gérer la transition de level correctement
	// Ne fonctionne pas correctement !
	void	OnClickStartServer()
	{
		if (data.gamePassword.Length > 0)
			Network.incomingPassword = data.gamePassword;
		if (data.gameName.Length == 0)
		{
			LogWarning("Impossible de creer une partie sans nom.");
			return;
		}
		
		if (Network.InitializeServer(data.gameMaxPlayers, data.gamePort, data.useNat) == NetworkConnectionError.NoError)
		{
			MasterServer.RegisterHost(data.gameType, data.gameName);
			LoadLevel();
			// TODO: De l'autre coté, gérer le bordel avec "OnLevelWasLoaded"
		}
		else
		{
			LogError("Impossible de creer une nouvelle partie.");
		}
	}
	
	private	void LoadLevel()
	{
		Application.LoadLevel("Game");
	}

	private void LogError(string msg)
	{
		MessageBox.Add("[FF3838]" + msg + "[-]"); // Rouge
		Debug.LogError(msg);
	}
	
	private void LogWarning(string msg)
	{
		MessageBox.Add("[E17417]" + msg + "[-]"); // Orange
		Debug.Log(msg);
	}
	
	private void Log(string msg)
	{
		MessageBox.Add("[44F058]" + msg + "[-]"); // Vert
	}
}
