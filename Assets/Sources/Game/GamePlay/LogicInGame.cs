/*
 * 
 * 
 * 
 *
 */

using						UnityEngine;
using						System.Collections;
using						System.Collections.Generic;

public class 				LogicInGame : MonoBehaviour
{
	public List<ALaunchEvent> _startEvent;

	private static LogicInGame _instance;
	public static LogicInGame Instance
	{ get { return (_instance); } }
	
	private List<GladiatorNetwork> _playerList;
	public List<GladiatorNetwork> PlayerList
	{
		get { return (_playerList); }
		set { _playerList = value; }
	}
	
	private bool			_wait = true;
	
	void					Awake()
	{
		_playerList = new List<GladiatorNetwork>();
	}
	
	void					Start()
	{
		_instance = this;
		PhotonNetwork.sendRate = 90;
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
		Spawn				spawn = GetMySpawn();
		GameObject			myPlayer = PhotonNetwork.Instantiate("NormalPlayer", spawn.transform.position, spawn.transform.rotation, 0);
		PhotonView			pv;
		
		myPlayer.name = RunTimeData.PlayerBase.PlayerName;
		pv = myPlayer.GetComponent<PhotonView>();
		pv.ownerId = PhotonNetwork.player.ID;
		HUDInitializer.Instance.init(myPlayer.GetComponent<GladiatorNetwork>());
	}
	
	Spawn					GetMySpawn()
	{
		List<Spawn>			spawnList = SpawnManager.Instance.SpawnList;
		
		foreach (Spawn spawn in spawnList)
			if (spawn.master == PhotonNetwork.isMasterClient)
				return (spawn);
		return (null);
	}
}
