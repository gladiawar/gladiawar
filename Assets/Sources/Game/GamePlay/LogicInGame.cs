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
	public GameObject[]		_particleText;
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

	private void CountDownTimer()
	{
		_countdownCounter--;
		if (_countdownCounter <= 0)
			this.CancelInvoke ("CountDownTimer");
		else if (_countdownCounter < 4)
			CFX_SpawnSystem.GetNextObject(_particleText[_countdownCounter - 1], true).transform.position = _igMessage.transform.position;
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
	
	public void				SpawnPlayer()
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

		GameObject myPlayer = InstantiateMyPlayer(spawn);
		PhotonView pv;
		
		myPlayer.name = RunTimeData.PlayerBase.PlayerName;
		pv = myPlayer.GetComponent<PhotonView> ();
		pv.ownerId = PhotonNetwork.player.ID;
		HUDInitializer.Instance.init(myPlayer.GetComponent<GladiatorNetwork>());
	}
	
	GameObject				InstantiateMyPlayer(Spawn spawn)
	{
		string[]			instantiateData = new string[3];
		instantiateData[0] = _teamNumber.ToString();
		instantiateData[1] = RunTimeData.PlayerBase.PlayerName;
		instantiateData[2] = ((uint)RunTimeData.PlayerBase.PlayerClass).ToString();
		
		object[] objs = new object[1];
		objs[0] = instantiateData;
		
		switch (RunTimeData.PlayerBase.PlayerClass)
		{
		case SelectClass.eClass.LIGHT:
			return (PhotonNetwork.Instantiate("LightPlayer", spawn.transform.position, spawn.transform.rotation, 0, objs));
		default:
			return (PhotonNetwork.Instantiate("NormalPlayer", spawn.transform.position, spawn.transform.rotation, 0, objs));
		}
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
		CFX_SpawnSystem.GetNextObject(_particleText[3], true).transform.position = _igMessage.transform.position;
		Invoke("finishingGame", 3f);
	}
	
	private void			finishingGame()
	{
		Screen.showCursor = true;
		PhotonNetwork.LeaveRoom();
		Application.LoadLevel("Hub");
	}
}
