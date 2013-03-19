using UnityEngine;
using System.Collections;

public class MasterServerHandler : MonoBehaviour 
{
	/*
	 * Au cas ou l'on voudrait changer les paramètres de configuration 
	 */
	public string	ipMasterServer = "10.224.9.214";
	public int		gamePort = 23466;
	
	public string	gameName = "GladiaWarArena";
	public string	serverName = "GWA_defaultName";
	public string	serverPasswd = "GWA_defaultPasswd";
	public string	serverIp = "127.0.0.1";
	
	// Use this for initialization
	void	Start () 
	{
		MasterServer.ipAddress = ipMasterServer;
		MasterServer.port = 23466;
	}
	
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
	}
	
	void	OnClickStartServer()
	{
	}
}
