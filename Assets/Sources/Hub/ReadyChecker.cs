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
				LogicInGame.Instance.InstantiateMasterServer();
				GameObject.Destroy(gameObject);
			}
		}
	}
}
