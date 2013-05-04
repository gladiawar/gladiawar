using UnityEngine;
using System.Collections;

public class GamesManagerData : MonoBehaviour 
{
	
	// L'adresse du serveur distant et son port
	public string 	gameIp = "127.0.0.1";
	public int		gamePort = 25000;
	
	// Le nombre maximum de joueurs dans une partie
	public int		gameMaxPlayers = 32;
	
	// Le nom de la partie
	public string	gameName = "Gladia tictac";
	
	// Mot de passe utilisée par la partie
	public string	gamePassword = "";
	
	// Option de connexion
	public bool		useNat = true;
	
	// L'adresse et le port du MasterServer (Celui qui liste les parties)
	public string	masterServerIp = "10.224.9.214";
	public int 		masterServerPort = 23466;

	// Le type de partie (utilisée par le MasterServer)
	public string	gameType = "GladiaWarArena_Game";
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	public void OnSubmitGameIp(string newGameIp)
	{
		gameIp = newGameIp;
	}
	
	public void OnSubmitGameName(string newGameName)
	{
		gameName = newGameName;
	}
	
	public void OnSubmitGamePassword(string newGamePassword)
	{
		gamePassword = newGamePassword;
	}
	
	public void PrintADebug()
	{
		Debug.Log("Print A Debug !");
	}
}
