/*
 * 
 * 
 * 
 *
 */
using						UnityEngine;
using						System.Collections;
using						System.Collections.Generic;

public class 				LogicInGame : Photon.MonoBehaviour
{
	public List<ALaunchEvent> _startEvent;
	private static LogicInGame _instance;

	public static LogicInGame Instance
	{ get { return (_instance); } }
	
	private List<GladiatorNetwork> _playerList;

	public List<GladiatorNetwork> PlayerList {
		get { return (_playerList); }
		set { _playerList = value; }
	}
	
	public UILabel    _igMessage;
	public int      _countdownValue = 5;
	private int      _countdownCounter = 0;
	private bool	_masterInstantiated = false;
	
	public void StartCountDown ()
	{
		_countdownCounter = _countdownValue;
		this.InvokeRepeating ("CountDownTimer", 0f, 1f);
	}

	private void CountDownTimer ()
	{
		if (_countdownCounter <= 0)
		{
			_igMessage.text = "";
			this.CancelInvoke ("CountDownTimer");
		}
		else
		{
			_countdownCounter--;
			_igMessage.text = _countdownCounter.ToString();
		}
	}
	
	void					Start ()
	{
		_instance = this;
		_playerList = new List<GladiatorNetwork>();
	}
	
	void					Update ()
	{
		if (!_masterInstantiated && PhotonNetwork.isMasterClient && PhotonNetwork.room.playerCount > 1)
		{
			_masterInstantiated = true;
			Invoke("InstantiateMasterServer", 4.0f);
		}
	}
	
	void					InstantiateMasterServer()
	{
		PhotonNetwork.Instantiate("MasterServer", transform.position, transform.rotation, 0);
	}
	
	public void				SpawnPlayer ()
	{
		Spawn spawn = GetMySpawn ();
		GameObject myPlayer = PhotonNetwork.Instantiate ("NormalPlayer", spawn.transform.position, spawn.transform.rotation, 0);
		PhotonView pv;
		
		myPlayer.name = RunTimeData.PlayerBase.PlayerName;
		pv = myPlayer.GetComponent<PhotonView> ();
		pv.ownerId = PhotonNetwork.player.ID;
		HUDInitializer.Instance.init (myPlayer.GetComponent<GladiatorNetwork> ());
	}
	
	Spawn					GetMySpawn ()
	{
		List<Spawn> spawnList = SpawnManager.Instance.SpawnList;
		
		foreach (Spawn spawn in spawnList)
			if (spawn.master == PhotonNetwork.isMasterClient)
				return (spawn);
		return (null);
	}
	
	public void				EndGame()
	{
		_igMessage.text = "Game terminated";
		Invoke("finishingGame", 3f);
	}
	
	private void			finishingGame()
	{
		Application.LoadLevel("Hub");
	}
}
