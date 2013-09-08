/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public class 				MasterServerData : Photon.MonoBehaviour
{
	bool					_gameStart = true;
	bool					_inCountDown = false;
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
		}
	}
	
	void					inGameStart()
	{
		if (PhotonNetwork.room.playerCount > 1)
		{
			_inCountDown = true;
			photonView.RPC("LaunchCountDown", PhotonTargets.All, null);
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
}
