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
	private bool			_onDefense = false;
	private float			_timeSinceAttackLaunch;
	private int				_animationPhase;
	private float[]			_animationPhaseTime;
	private bool			_haveTouched;
	private int				_layerGladiator;
	
	const int 				_attackCost = 20;
	const int				_defenseCost = 50;
	
	float					_defenseEnergySpend = 0;
	
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
		if (GladiatorNetwork._myGladiator.Life < 1)
			return ;
		if (_onAttack)
			MajAttack();
		else if (_onDefense)
			MajDefense();
		else if (Input.GetMouseButtonDown(0))
			LaunchAttack();
		else if (Input.GetMouseButtonDown(1))
			LaunchDefense();
	}
	
	void					LaunchAttack()
	{
		if (_gladiatorNetwork.Life == 0 || _gladiatorNetwork.Energy < _attackCost)
			return ;
		_gladiatorNetwork.Energy -= _attackCost;
		_animMngr.State = AnimationStateManager.eState.ATTACK;
		_onAttack = true;
		_onDefense = false;
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
	
	void					LaunchDefense()
	{
		if (_gladiatorNetwork.Energy > 25 && !_onAttack)
		{
			_onDefense = true;
			_animMngr.State = AnimationStateManager.eState.DEFENSE;
		}
	}
	
	void					MajDefense()
	{
		if (Input.GetMouseButtonUp(1) || _gladiatorNetwork.Energy < 5)
		{
			_onDefense = false;
			_animMngr.State = AnimationStateManager.eState.IDLE;
		}
		else
		{
			int				intEnergySpend;
			
			_defenseEnergySpend += Time.deltaTime * _defenseCost;
			intEnergySpend = (int)_defenseEnergySpend;
			_gladiatorNetwork.Energy -= intEnergySpend;
			_defenseEnergySpend -= intEnergySpend;
		}
	}
}
