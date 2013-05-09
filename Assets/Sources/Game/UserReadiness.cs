using UnityEngine;
using System.Collections;

// TODO: Renommer cette merde.
public class UserReadiness : MonoBehaviour 
{
	public bool				Status = false;
	
	// Le label du bouton ready
	public UILabel			buttonReadyLabel;
	
	// Le script qui a une netview associé et qui balance les RPC
	public GameChatRoomManager	gameChatRoomManager;
	
	// Les infos du joueur
	private PlayerInfo		playerInfo;
	
	void	Awake()	
	{
	}
	
	void	Start()
	{
		playerInfo = GameObject.Find("PlayerPrefs").GetComponent<PlayerInfo>();
		gameChatRoomManager.SetStatus(playerInfo.GetPlayerName(), Status);
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	void	OnClickButtonReady()
	{
		Status = !Status;
		
		if (Status)
		{
			buttonReadyLabel.text = "Ready";
		}
		else
		{
			buttonReadyLabel.text = "Not Ready";
		}
		gameChatRoomManager.SetStatus(playerInfo.GetPlayerName(), Status);
	}
}
