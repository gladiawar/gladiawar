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
	const float				_pingTime = 2f;
	float					_elapsedTime = 0;
	
	private bool			_didClientRequest = false;
	private List<string>	_player1;
	private List<string>	_player2;
	private List<UILabel>	_labelPlayer1;
	private List<UILabel>	_labelPlayer2;
	
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
			_player1.Add(RunTimeData.PlayerBase.PlayerName);
		MajDisplay();
	}
	
	void					Update()
	{
		if ((_elapsedTime += Time.deltaTime) > _pingTime)
		{
			if (photonView.isMine)
				sendTeamInfo();
			else if (!_didClientRequest)
				photonView.RPC("ClientTeamAsk", PhotonTargets.MasterClient, RunTimeData.PlayerBase.PlayerName);
			_elapsedTime = 0;
		}
	}
	
	void					sendTeamInfo()
	{
		string				data = parsePlayerList(_player1, (0).ToString()) + "/" + parsePlayerList(_player2, (1).ToString());
		
		photonView.RPC("ReceiveTeamInfo", PhotonTargets.Others, data);
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
		GameObject.Destroy(this);
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
		}
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
			
			if (_didClientRequest && playerData[0] == RunTimeData.PlayerBase.PlayerName)
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
}
