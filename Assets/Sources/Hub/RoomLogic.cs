/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;
using						System.Collections.Generic;

public class 				RoomLogic : Photon.MonoBehaviour
{
	const float				_pingTime = 0.5f;
	float					_elapsedTime = 0;
	
	private Transform		_InputBox;
	private UILabel			_messageBox;
	private string			_currentMap;
	private bool			_didClientRequest = false;
	private List<string>	_player1;
	private List<string>	_player2;
	private List<UILabel>	_labelPlayer1;
	private List<UILabel>	_labelPlayer2;
	private List<string>	_messages;

	void					Start()
	{
		_player1 = new List<string>();
		_player2 = new List<string>();
		
		_labelPlayer1 = new List<UILabel>();
		_labelPlayer1.Add(GameObject.Find("LabelPlayer11").GetComponent<UILabel>());
		_labelPlayer1.Add(GameObject.Find("LabelPlayer12").GetComponent<UILabel>());
		_labelPlayer1.Add(GameObject.Find("LabelPlayer13").GetComponent<UILabel>());
		
		_labelPlayer2 = new List<UILabel>();
		_labelPlayer2.Add(GameObject.Find("LabelPlayer21").GetComponent<UILabel>());
		_labelPlayer2.Add(GameObject.Find("LabelPlayer22").GetComponent<UILabel>());
		_labelPlayer2.Add(GameObject.Find("LabelPlayer23").GetComponent<UILabel>());
		
		if (photonView.isMine)
		{
			_player1.Add(RunTimeData.PlayerBase.PlayerName);
			RunTimeData.PlayerTeam = 0;
		}
		_messageBox = GameObject.Find("Messages").GetComponent<UILabel>();
		_messages = new List<string>();
		_InputBox = GameObject.Find("playerInput").transform;
		_InputBox.GetComponent<UIInput>().eventReceiver = gameObject;
		MajDisplay();
	}
	
	void					Update()
	{
		if ((_elapsedTime += Time.deltaTime) > _pingTime)
		{
			if (!photonView.isMine && !_didClientRequest)
				photonView.RPC("ClientTeamAsk", PhotonTargets.MasterClient, RunTimeData.PlayerBase.PlayerName);
			_elapsedTime = 0;
		}
		if (photonView.isMine)
		{
			if (RunTimeData.MapName != _currentMap)
			{
				_currentMap = RunTimeData.MapName;
				sendMapName();
			}
		}
	}
	
	void					sendTeamInfo()
	{
		string				data = parsePlayerList(_player1, (0).ToString()) + "/" + parsePlayerList(_player2, (1).ToString());
		
		photonView.RPC("ReceiveTeamInfo", PhotonTargets.Others, data);
	}
	
	void					sendMapName()
	{
		photonView.RPC("UpdateMapName", PhotonTargets.Others, _currentMap);
	}
	
	string					parsePlayerList(List<string> playerList, string teamNumber)
	{
		int					count = 0;
		string				data = "";
		
		foreach (string player in playerList)
		{
			if (count > 0)
				data += "/";
			data += player + ";" + teamNumber + ";" + count.ToString();
			++count;
		}
		return (data);
	}
	
	void					OnDisable()
	{
		PhotonNetwork.Destroy(gameObject);
	}
	
	void					MajDisplay()
	{
		int					count = 0;
		
		foreach (UILabel label in _labelPlayer1)
			label.text = "";
		foreach (UILabel label in _labelPlayer2)
			label.text = "";
		foreach (string pname in _player1)
			_labelPlayer1[count++].text = pname;
		count = 0;
		foreach (string pname in _player2)
			_labelPlayer2[count++].text = pname;
	}
	
	[RPC]
	void					ClientTeamAsk(string pname)
	{
		if (!playerExist(pname))
		{
			if (_player2.Count < _player1.Count)
				_player2.Add(pname);
			else
				_player1.Add(pname);
			MajDisplay();
			sendTeamInfo();
			sendMapName();
		}
		if (PhotonNetwork.room.playerCount == 6)
			PhotonNetwork.room.open = false;
	}
	
	[RPC]
	void					UpdateMapName(string mname)
	{
		RunTimeData.MapName = mname;
	}
	
	bool					playerExist(string pname)
	{
		foreach (string playerName in _player1)
			if (playerName == pname)
				return (true);
		foreach (string playerName in _player2)
			if (playerName == pname)
				return (true);
		return (false);
	}
	
	[RPC]
	void					ReceiveTeamInfo(string data)
	{
		char[]				separator = { '/' };
		char[]				separator2 = { ';' };
		string[]			playerList = data.Split(separator);
		
		_player1.Clear();
		_player2.Clear();
		for (int i = 0; i < playerList.Length; ++i)
		{
			if (playerList[i].Length < 5)
				continue;
			string[]		playerData = playerList[i].Split(separator2);
			int				pteam = int.Parse(playerData[1]);
			int				pslot = int.Parse(playerData[2]);
			
			if (!_didClientRequest && playerData[0] == RunTimeData.PlayerBase.PlayerName)
			{
				_didClientRequest = true;
				RunTimeData.PlayerTeam = pteam;
				RunTimeData.PlayerSlot = pslot;
			}
			if (pteam == 0)
			{
				_player1.Add(playerData[0]);
				_labelPlayer1[pslot].text = playerData[0];
			}
			else
			{
				_player2.Add(playerData[0]);
				_labelPlayer2[pslot].text = playerData[0];
			}
		}
		MajDisplay();
	}
	
#region chat
	void					OnSubmit(string message)
	{
		photonView.RPC("getMessage", PhotonTargets.All, "[" + RunTimeData.PlayerBase.PlayerName + "]: " + message);
	}
	
	[RPC]
	void					getMessage(string message)
	{
		_messages.Add(message);
		while (_messages.Count > 13)
			_messages.Remove(_messages[0]);
		refreshChat();
	}
	
	void					refreshChat()
	{
		int					count = 0;
		string				finalText = "";
		
		foreach (string msg in _messages)
		{
			if (count++ > 0)
				finalText += "\n";
			finalText += msg;
		}
		_messageBox.text = finalText;
	}
#endregion
}
