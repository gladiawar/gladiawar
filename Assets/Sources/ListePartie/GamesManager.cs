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
	
	// Use this for initialization
	void	Start () 
	{		
		MasterServer.ipAddress = data.masterServerIp;
		MasterServer.port = data.masterServerPort;
		MasterServer.ClearHostList();
		MasterServer.RequestHostList(data.gameType);
	}
	
	// TODO: Gérer la connection à la partie distante (Connection + Changement de level)
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
			Debug.LogError("Error while Connecting to server : " + data.gameIp + "/" + data.gamePort);
		}
	}
	
	void	OnClickConnect()
	{
		ConnectToServer(data.gameIp, data.gamePort);
	}
	
	private void SearchGames()
	{
		MasterServer.RequestHostList(data.gameType);
		hostList = MasterServer.PollHostList();
	}
	
	private UILabel GetNewUIButtonLabel(GameObject Parent)
	{
		Transform	refT = UIButtonToClone.transform;
		GameObject 	go = (GameObject)UILabel.Instantiate(	UIButtonToClone, 
															new Vector3(refT.position.x, refT.position.y, refT.position.z), 
															new Quaternion(0, 0, 0, 0));
		go.transform.parent = Parent.transform;
		go.transform.localScale = refT.localScale;
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
			UILabel label = this.GetNewUIButtonLabel(PanelServerList);
			label.transform.parent.position -= new Vector3(0, (float)nbServer / 14f, 0);	// Allez savoir pourquoi 16 ....
			label.transform.parent.name = "LabelGame";
			string	text = "[00FF00]" + hd.gameName + "[-] " + hd.connectedPlayers + "/" + hd.playerLimit + " [";
			// hd est un string[]
			
			string ip = (string)(hd.ip.GetValue(0));
			text += ip;
			label.transform.parent.GetComponent<OnClickJoinGame>().gameIp = ip;
			text += "]";
			label.text = text;

			nbServer++;
		}
	}
	
	// TODO: Gérer la transition de level correctement
	// Ne fonctionne pas correctement !
	void	OnClickStartServer()
	{
		if (data.gamePassword.Length > 0)
			Network.incomingPassword = data.gamePassword;
		
		if (Network.InitializeServer(data.gameMaxPlayers, data.gamePort, data.useNat) == NetworkConnectionError.NoError)
		{
			MasterServer.RegisterHost(data.gameType, data.gameName);
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
		Application.LoadLevel("Game");
	}
}
