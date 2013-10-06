/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;
using						System.Collections.Generic;

public class 				GladiatorNetwork : Photon.MonoBehaviour
{
	GladiatorCamera			_cam;
	GladiatorController		_ctrler;
	AttackEventManager		_attackEventManager;
	Vector3					_playerPos;
	Quaternion				_playerRot;
	CharacterController		_charCtrl;
	AnimationStateManager	_animationManager;
	SelectClass.eClass		_class;
	
	int						_teamNb;
	public int				TeamNb
	{
		get { return (_teamNb); }
	}
	
	int						_life = 100;
	public int				Life
	{
		get { return (_life); }
		set
		{
			_life = value;
			if (_life <= 0)
			{
				_life = 0;
				_animationManager.State = AnimationStateManager.eState.DIE;
			}
			else
				_animationManager.State = AnimationStateManager.eState.RECEIVEATTACK;
		}
	}
	int						_kill = 0;
	public int				Kill
	{
		get { return _kill; }
		set { _life = value; }
	}
	
	int						_death = 0;
	public int				Death
	{
		get { return _death; }
		set { _death = value; }
	}
	
	float					_timeForRegen = 0.1f;
	float					_elapsedTimeSinceRegen = 0;
	float					_runningEnergySpend = 0;
	int						_energy = 100;
	public int				Energy
	{
		get { return (_energy); }
		set
		{
			if ((_energy = value) > 100)
				_energy = 100;
			else if (_energy < 0)
				_energy = 0;
		}
	}
	
	public static GladiatorNetwork _myGladiator;
	
	void					Awake()
	{
		_cam = transform.GetComponent<GladiatorCamera>();
		_ctrler = transform.GetComponent<GladiatorController>();
		_attackEventManager = transform.GetComponent<AttackEventManager>();
		_charCtrl = transform.GetComponent<CharacterController>();
		_animationManager = transform.GetComponent<AnimationStateManager>();
	}
	
	void 					Start()
	{
		LogicInGame.Instance.PlayerList.Add(this);
		if (photonView.isMine)
			_myGladiator = this;
		else
		{
			((MonoBehaviour)_cam).enabled = false;
			((MonoBehaviour)_ctrler).enabled = false;
			((MonoBehaviour)_attackEventManager).enabled = false;
			_charCtrl.enabled = false;
			_animationManager.Remote = true;
			CapsuleCollider cc = gameObject.AddComponent<CapsuleCollider>();
			cc.radius = 0.4f;
			cc.height = 1.4f;
			cc.center = new Vector3(0, 0.6f, 0);
		}
	}
	
	void 					Update()
	{
		if (!photonView.isMine)
		{
			transform.position = Vector3.Lerp(transform.position, _playerPos, Time.deltaTime * 5);
			transform.rotation = Quaternion.Lerp(transform.rotation, _playerRot, Time.deltaTime * 5);
		}
		else if ((_elapsedTimeSinceRegen += Time.deltaTime) > _timeForRegen)
		{
			int			nb = (int)(_elapsedTimeSinceRegen / _timeForRegen);
				
			Energy += nb;
			_elapsedTimeSinceRegen -= (_timeForRegen * nb);
		}
	}
	
	void 					OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			stream.SendNext((int)_animationManager.State);

		}
		else
		{
			AnimationStateManager.eState state;
			
			_playerPos = (Vector3)stream.ReceiveNext();
			_playerRot = (Quaternion)stream.ReceiveNext();
			state = (AnimationStateManager.eState)stream.ReceiveNext();
			if (_animationManager.State != state)
				_animationManager.State = state;

		}
	}
	
	public void				SendAttack(GladiatorNetwork other)
	{
		other.photonView.RPC("ReceiveAttack", PhotonTargets.All, null);
	}
	
	[RPC]
	public void				ReceiveAttack()
	{
		if (_animationManager.State == AnimationStateManager.eState.DEFENSE)
		{
			Life -= 2;
			Energy -= 20;
		}
		else
			Life -= 10;
	}
	
	public bool				isRunning(float energyRequire)
	{
		int					spendInt;
		
		if (_energy < 10)
		{
			_animationManager.State = AnimationStateManager.eState.WALK;
			return (false);
		}
		_runningEnergySpend += energyRequire;
		spendInt = (int)_runningEnergySpend;
		_energy -= spendInt;
		_runningEnergySpend -= (float)spendInt;
		return (true);
	}
	
	void 					OnPhotonInstantiate(PhotonMessageInfo info)
	{
		if (!photonView.isMine)
		{
			GameObject		HUDText = NGUITools.AddChild(LogicInGame.Instance._HUDText.gameObject, LogicInGame.Instance._FollowerPrefab);
			object[]		objs = photonView.instantiationData;
			string[]		idata = (string[])objs[0];
			
			Debug.Log("is not mine");
			_teamNb = int.Parse(idata[0]);
			_class = (SelectClass.eClass)uint.Parse(idata[2]);
			HUDText.GetComponent<UIFollowTarget>().target = transform.FindChild("TextPosition");
			HUDText.GetComponent<UIFollowTarget>().uiCamera = LogicInGame.Instance._UICamera;
			HUDText.transform.GetChild(0).GetComponent<UILabel>().text = idata[1];
			HUDText.transform.GetChild(0).GetComponent<UILabel>().color = (_teamNb == 0 ? Color.blue : Color.red);
		}
		else
		{
			Debug.Log("is mine");
			_class = RunTimeData.PlayerBase.PlayerClass;
		}
	}
	
	void					OnDestroy()
	{
	}
}
