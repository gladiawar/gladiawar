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
	public GameObject		_endMessage;
	public UILabel		_timerMessage;
	public float			_countdownValue = 10f;
	private float			_countdownCounter = 0f;
	private static LogicInGame _instance;

	public static LogicInGame Instance
	{ get { return (_instance); } }
	
	private List<GladiatorNetwork>	_playerList;

	public List<GladiatorNetwork> PlayerList {
		get { return (_playerList); }
		set { _playerList = value; }
	}
	
	private enum GameState
	{
		AWAITING,
		COUNTDOWN,
		PENDING,
		RUNNING,
		ENDED
	};
	
	private GameState _gameState;
	
	
//  Deprecated
//	private IEnumerator CountDown ()
//	{
//		yield return new WaitForSeconds(_countdownValue);
//		_gameState = GameState.RUNNING;
//	}
	
	private void StartCountDown ()
	{
		if (_countdownCounter > 0 || this.IsInvoking ("CountDownTimer")) {
			return;
		}
		_countdownCounter = _countdownValue;
		this.InvokeRepeating ("CountDownTimer", 0f, 1f);
	}

	private void CountDownTimer ()
	{
		if (_countdownCounter <= 0) {
			_timerMessage.gameObject.SetActive (false);
			_gameState = GameState.RUNNING;
			this.CancelInvoke ("CountDownTimer");
		} else {
			_countdownCounter--;
			_timerMessage.text = "Warmup : " + _countdownCounter + " second(s) left.";
		}
	}
	
	void					Awake ()
	{
		_instance = this;
		_playerList = new List<GladiatorNetwork> ();
		_gameState = GameState.AWAITING;
		_timerMessage.gameObject.SetActive (false);
	}
	
	void					Start ()
	{
		PhotonNetwork.sendRate = 90;
		_timerMessage.gameObject.SetActive (false);
	}
	
	void					Update ()
	{
		
//			foreach (ALaunchEvent le in _startEvent)
//			{
//				le.launchCountDown();
//				le.allPlayerInstantiated();
//				le.launchGame();
//			}	

		//FFA impl. Game's ending when 1 player left.
		
		switch (_gameState) {
		case GameState.AWAITING:
			if (PhotonNetwork.room.playerCount > 1) {
//				Invoke("SpawnPlayer", 3.0f);
				SpawnPlayer ();
				
				_timerMessage.gameObject.SetActive (true);
				StartCountDown ();
				_gameState = GameState.COUNTDOWN;
			}
			break;
		case GameState.PENDING:
			break;
		case GameState.COUNTDOWN:
			break;
		case GameState.RUNNING:
			if (PhotonNetwork.room.playerCount == _playerList.Count) {
				int count = 0;
				foreach (GladiatorNetwork gn in _playerList)
					count = (gn.Life > 0) ? count + 1 : count;
				
				if (count <= 1)
					_gameState = GameState.ENDED;
			}
			break;
		case GameState.ENDED:
			_endMessage.SetActive (true);
			break;
		default:
			break;
		}
	}
	
	void					SpawnPlayer ()
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
}
