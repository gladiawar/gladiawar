/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;
using						System.Collections.Generic;

public class 				MasterServerData : Photon.MonoBehaviour
{
	bool					_gameStart = true;
	bool					_inCountDown = false;
	bool					_gameEnded = false;
	float					_countDownTime = 0;
	
	void					Update()
	{
		if (photonView.isMine)
		{
			if (_gameStart)
				inGameStart();
			else if (_inCountDown)
			{
				if ((_countDownTime += Time.deltaTime) >= 5f)
					LaunchGame();
			}
			else if (!_gameEnded)
				checkEndGame();
		}
	}
	
	public void				inGameStart()
	{
		_inCountDown = true;
		photonView.RPC("LaunchCountDown", PhotonTargets.All, null);
	}
	
	void					checkEndGame()
	{
		List<GladiatorNetwork> list = LogicInGame.Instance.PlayerList;
		int					alive1 = 0;
		int					alive2 = 0;
		
		foreach (GladiatorNetwork glad in list)
		{
			if (glad.Life > 0 && glad.TeamNb == 0)
				++alive1;
			else if (glad.Life > 0 && glad.TeamNb == 1)
				++alive2;
		}
		if (alive1 == 0 || alive2 == 0)
		{
			_gameEnded = true;
			photonView.RPC("EndGame", PhotonTargets.All, null);
		}
	}
	
	[RPC]
	void					LaunchCountDown()
	{
		_gameStart = false;
		LogicInGame.Instance.SpawnPlayer();
		LogicInGame.Instance.StartCountDown();
	}
	
	[RPC]
	void					LaunchGame()
	{
		_inCountDown = false;
	}
	
	[RPC]
	void					EndGame()
	{
		LogicInGame.Instance.EndGame();
	}
	
	void					OnDestroy()
	{
		PhotonNetwork.LeaveRoom();
	}
}
