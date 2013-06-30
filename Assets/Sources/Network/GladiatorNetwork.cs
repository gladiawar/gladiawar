/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public class 				GladiatorNetwork : Photon.MonoBehaviour
{
	ThirdPersonCamera		_cam;
	ThirdPersonController	_ctrler;
	AttackEventManager		_attackEventManager;
	Vector3					_playerPos;
	Quaternion				_playerRot;
	CharacterController		_charCtrl;
	AnimationStateManager	_animationManager;
	
	void					Awake()
	{
		_cam = transform.GetComponent<ThirdPersonCamera>();
		_ctrler = transform.GetComponent<ThirdPersonController>();
		_attackEventManager = transform.GetComponent<AttackEventManager>();
		_charCtrl = transform.GetComponent<CharacterController>();
		_animationManager = transform.GetComponent<AnimationStateManager>();
	}
	
	void 					Start()
	{
		if (photonView.isMine)
		{
			_cam.cameraTransform = Camera.mainCamera.transform;
		}
		else
		{
			((MonoBehaviour)_cam).enabled = false;
			((MonoBehaviour)_ctrler).enabled = false;
			((MonoBehaviour)_attackEventManager).enabled = false;
			_charCtrl.enabled = false;
			_animationManager.Remote = true;
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
			stream.SendNext(_charCtrl.velocity);
		}
		else
		{
			_playerPos = (Vector3)stream.ReceiveNext();
			_playerRot = (Quaternion)stream.ReceiveNext();
			_animationManager.RemoteVelocity = (Vector3)stream.ReceiveNext();
		}
	}
}
