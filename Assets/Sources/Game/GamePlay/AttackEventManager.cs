/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public class 				AttackEventManager : MonoBehaviour
{
	public GladiatorNetwork	_gladiatorNetwork;
	private AnimationStateManager _animMngr;
	private bool			_onAttack = false;
	private float			_timeSinceAttackLaunch;
	private int				_animationPhase;
	private float[]			_animationPhaseTime;
	private bool			_haveTouched;
	private int				_layerGladiator;
	
	void 					Start()
	{
		_animMngr = transform.GetComponent<AnimationStateManager>();
		_animationPhaseTime = new float[4];
		_animationPhaseTime[0] = 0.37f;
		_animationPhaseTime[1] = 0.44f;
		_animationPhaseTime[2] = 0.505f;
		_animationPhaseTime[3] = 0.833f;
		_layerGladiator = 1 << 9;
	}
	
	void 					Update()
	{
		if (_onAttack)
			MajAttack();
		else if (Input.GetMouseButtonDown(0))
			LaunchAttack();
	}
	
	void					LaunchAttack()
	{
		if (_gladiatorNetwork.Life == 0)
			return ;
		_animMngr.State = AnimationStateManager.eState.ATTACK;
		_onAttack = true;
		_haveTouched = false;
		_timeSinceAttackLaunch = 0;
		_animationPhase = 0;
	}
	
	void					MajAttack()
	{
		_timeSinceAttackLaunch += Time.deltaTime;
		if (_timeSinceAttackLaunch >= _animationPhaseTime[_animationPhase])
		{
			if (_animationPhase == 3)
				_onAttack = false;
			else
			{
				if (!_haveTouched)
					LaunchRaycast();
				++_animationPhase;
			}
		}
	}
	
	void					LaunchRaycast()
	{
		Vector3				direction;
		RaycastHit			rcData;
		
		switch (_animationPhase)
		{
		case 0:
			direction = transform.TransformDirection(new Vector3(0.35f, 0, 0.65f)); break;
		case 1:
			direction = transform.TransformDirection(Vector3.forward); break;
		default:
			direction = transform.TransformDirection(new Vector3(-0.35f, 0, 0.65f)); break;
		}
		if (Physics.Raycast(transform.position, direction, out rcData, 0.9f, _layerGladiator))
		{
			if (rcData.collider.gameObject != this.gameObject)
			{
				_gladiatorNetwork.SendAttack(rcData.collider.gameObject.transform.GetComponent<PhotonView>());
				_haveTouched = true;
			}	
		}
	}
}
