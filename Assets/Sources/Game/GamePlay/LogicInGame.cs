/*
 * 
 * 
 * 
 *
 */

using						UnityEngine;
using						System.Collections;

public class 				LogicInGame : MonoBehaviour
{
	private bool			_wait = true;
	
	void					Start()
	{
	}
	
	void					Update()
	{
		if (_wait && PhotonNetwork.room.playerCount > 1)
		{
			Invoke("SpawnPlayer", 3.0f);
			_wait = false;
		}
	}
	
	void					SpawnPlayer()
	{
		GameObject			myPlayer = PhotonNetwork.Instantiate("NormalPlayer", new Vector3(11, 0.65f, -25), Quaternion.Euler(new Vector3(0, 0, 0)), 0);
		PhotonView			pv;
		
		pv = myPlayer.GetComponent<PhotonView>();
		pv.ownerId = PhotonNetwork.player.ID;
	}
}
