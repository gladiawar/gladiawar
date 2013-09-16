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
	
	public Camera			_UICamera;
	public UIAnchor			_HUDText;
	public GameObject		_FollowerPrefab;
	public UILabel    		_igMessage;
	public int      		_countdownValue = 5;
	private int      		_countdownCounter = 0;
	
	private int				_teamNumber;
	public int				TeamNumber
	{
		get { return (_teamNumber); }
		set { _teamNumber = value; }
	}
	
	private int				_teamSlot;
	public int				TeamSlot
	{
		get { return (_teamSlot); }
		set { _teamSlot = value; }
	}
	
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
		_teamNumber = RunTimeData.PlayerTeam;
		_teamSlot = RunTimeData.PlayerSlot;
	}
	
	public void				InstantiateMasterServer()
	{
		PhotonNetwork.Instantiate("MasterServer", transform.position, transform.rotation, 0);
	}
	
	public void				SpawnPlayer ()
	{
		Spawn 				spawn = GetMySpawn();
		Vector3				spawnPos = spawn.transform.position;
		
		switch (_teamSlot)
		{
		case 0:
			spawnPos += new Vector3(6, 0, 0); break;
		case 2:
			spawnPos += new Vector3(-6, 0, 0); break;
		}
		
		GameObject myPlayer = PhotonNetwork.Instantiate ("NormalPlayer", spawn.transform.position, spawn.transform.rotation, 0);
		PhotonView pv;
		
		myPlayer.name = RunTimeData.PlayerBase.PlayerName;
		pv = myPlayer.GetComponent<PhotonView> ();
		pv.ownerId = PhotonNetwork.player.ID;
		HUDInitializer.Instance.init (myPlayer.GetComponent<GladiatorNetwork> ());
	}
	
	Spawn					GetMySpawn()
	{
		List<Spawn> spawnList = SpawnManager.Instance.SpawnList;
		
		Debug.Log(_teamNumber.ToString());
		foreach (Spawn spawn in spawnList)
		{
			if (_teamNumber == 0)
			{
				if (spawn.master)
					return (spawn);
			}
			else
			{
				if (!spawn.master)
					return (spawn);
			}
		}
		return (null);
	}
	
	public void				EndGame()
	{
		_igMessage.text = "Game terminated";
		Invoke("finishingGame", 3f);
	}
	
	private void			finishingGame()
	{
		Screen.showCursor = true; 
		Application.LoadLevel("Hub");
	}
}
