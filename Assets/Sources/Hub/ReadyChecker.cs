/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;
using						System.Collections.Generic;

public class 				ReadyChecker : Photon.MonoBehaviour
{
	const float				_pingTime = 1f;
	const int				_playerNumber = 2;
	float					_elapsedTime;
	
	List<string>			_playerReady;
	
	void					Start()
	{
		DontDestroyOnLoad(gameObject);
		if (photonView.isMine)
			_playerReady = new List<string>();
		Application.LoadLevel("Map1");
	}
	
	void					Update()
	{
		if (!photonView.isMine && Application.loadedLevelName != "Hub")
		{
			if ((_elapsedTime += Time.deltaTime) >= _pingTime)
			{
				_elapsedTime = 0;
				photonView.RPC("ready", PhotonTargets.MasterClient, RunTimeData.PlayerBase.PlayerName);
			}
		}
	}
	
	[RPC]
	void					ready(string playerName)
	{
		bool				unknown = true;
		
		foreach (string pnl in _playerReady)
			if (pnl == playerName)
				unknown = false;
		if (unknown)
		{
			_playerReady.Add(playerName);
			if (_playerReady.Count == _playerNumber - 1)
			{	
				LaunchTeamInfo();
				LogicInGame.Instance.InstantiateMasterServer();
			}
		}
	}
	
	void					LaunchTeamInfo()
	{
		string				info = "";
		int					playerNumber = _playerReady.Count;
		int					teamNumber;
		int					count = 1;
		
		foreach (string player in _playerReady)
		{
			if (count > 1)
				info += "/";
			info += player + ";";
			teamNumber = ((count < (playerNumber / 2)) ? 0 : 1);
			info += teamNumber.ToString();
			info += ";";
			info += (count - (teamNumber * (playerNumber / 2))).ToString();
			++count;
		}
		photonView.RPC("destroyChecker", PhotonTargets.All, info);
	}
			
	[RPC]
	void					destroyChecker(string teamInfo)
	{
		if (photonView.isMine)
		{
			LogicInGame.Instance.TeamNumber = 0;
			LogicInGame.Instance.TeamSlot = 0;
		}
		else
		{
			char[]			separator = { '/' };
			char[]			separator2 = { ';' };
			string[]		players = teamInfo.Split(separator);
			
			for (int i = 0; i < players.Length; ++i)
			{
				string[]	data = players[i].Split(separator2);
				
				if (data[0] == RunTimeData.PlayerBase.PlayerName)
				{
					LogicInGame.Instance.TeamNumber = int.Parse(data[1]);
					LogicInGame.Instance.TeamSlot = int.Parse(data[2]);
					break;
				}
			}
		}
		GameObject.Destroy(gameObject);
	}
}
