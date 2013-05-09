using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameChatRoomManager : MonoBehaviour 
{
	private Dictionary<string, bool>	userReadyness;
	public	UILabel						ModeleLabel;
	public  UITextList					chatTextZone;
	private int							labelNumber = 0;
	
	void Awake()
	{
		userReadyness = new Dictionary<string, bool>();
		networkView.group = 2;
	}
	
	// Use this for initialization
	void Start () 
	{
	}

	// Set status for a player
	public void	SetStatus(string userName, bool status)
	{
		networkView.RPC("RPC_SetStatus", RPCMode.All, userName, status);
	}
	
	[RPC]
	private void	RPC_SetStatus(string userName, bool status)
	{
		if (userReadyness.ContainsKey(userName) == false)
			userReadyness.Add(userName, status);
		else
			userReadyness[userName] = status;
		UpdatePanel();
	}

	// Add text to the chat box
	public void	OnSubmitChatText(string message)
	{
		networkView.RPC("RPC_OnSubmitChatText", RPCMode.All, PlayerInfo.playerInfo.GetPlayerName(), message);
	}

	[RPC]
	private void	RPC_OnSubmitChatText(string author, string message)
	{
		chatTextZone.Add("[00FF00]" + author + "[-] : " + message);
	}

	private void UpdatePanel()
	{
		foreach (KeyValuePair<string, bool> pair in userReadyness)
		{
			UILabel label = GetUILabelFromUsername(pair.Key);
			if (pair.Value == true)
			{
				label.color = Color.green;
			}
			else
			{
				label.color = Color.red;
			}
		}
	}
	
	private UILabel	GetUILabelFromUsername(string userName)
	{
		UILabel		label;
		Transform	tf = transform.FindChild("StatusUser_" + userName);
		
		if (tf == null)
		{
			label = NGUITools.AddWidget<UILabel>(ModeleLabel.transform.parent.gameObject);
			
			label.font = ModeleLabel.font;
			label.transform.localPosition = ModeleLabel.transform.localPosition - new Vector3(0, 25 * labelNumber, 0);
			label.transform.localScale = ModeleLabel.transform.localScale;
			label.effectStyle = ModeleLabel.effectStyle;
			label.pivot = ModeleLabel.pivot;
			label.name = "StatusUser_" + userName;
			label.text = userName;
			label.depth = ModeleLabel.depth + labelNumber;
			label.transform.gameObject.SetActive(true);
			++labelNumber;
		}
		else
			label = tf.GetComponent<UILabel>();
		return (label);
	}

	private void	DeleteUILabelFromUsername(string userName)
	{
		// TODO: Supprimer le label + réorganiser les autres
	}

	// Messages from Network
	void OnPlayerConnected(NetworkPlayer pl)
	{
		foreach (KeyValuePair<string, bool> pair in userReadyness)
		{
			SetStatus(pair.Key, pair.Value);
		}
	}
	
	// TODO: Faire OnDisconnectedFromServer
}
