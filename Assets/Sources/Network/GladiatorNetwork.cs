/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public class 				GladiatorNetwork : Photon.MonoBehaviour
{
	GladiatorCamera			_cam;
	GladiatorController		_ctrler;
	AttackEventManager		_attackEventManager;
	Vector3					_playerPos;
	Quaternion				_playerRot;
	CharacterController		_charCtrl;
	AnimationStateManager	_animationManager;
	int						_playerTouched = -1;
	
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
	}
	
	void 					OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			stream.SendNext((int)_animationManager.State);
			stream.SendNext(_playerTouched);
			_playerTouched = -1;
		}
		else
		{
			int				ptd;
			AnimationStateManager.eState state;
			
			_playerPos = (Vector3)stream.ReceiveNext();
			_playerRot = (Quaternion)stream.ReceiveNext();
			state = (AnimationStateManager.eState)stream.ReceiveNext();
			if (_animationManager.State != state)
				_animationManager.State = state;
			ptd = (int)stream.ReceiveNext();
			if (ptd > -1)
			{
				PhotonView	pv = PhotonView.Find(ptd);
				
				if (pv.isMine)
					GladiatorNetwork._myGladiator.ReceiveAttack();
			}
		}
	}
	
	
	public void				SendAttack(PhotonView other)
	{
		_playerTouched = other.viewID;
	}
	
	public void				ReceiveAttack()
	{
		Life -= 10;
	}
}
