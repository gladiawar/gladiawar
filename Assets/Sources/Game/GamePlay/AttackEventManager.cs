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
	private GladiatorController	_gldCtrl;
	private int				_damage;
	private float			_rcDistance;
	
	const int 				_attackCost = 15;
	const int				_defenseCost = 15;
	
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
		_gldCtrl = transform.GetComponent<GladiatorController>();
		switch (GladiatorNetwork._myGladiator.Class)
		{
		case SelectClass.eClass.LIGHT:
			_damage = 8;
			_rcDistance = 1.3f; break;
		default:
			_damage = 10;
			_rcDistance = 0.9f; break;
		}
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
		if (Physics.Raycast(transform.position, direction, out rcData, _rcDistance, _layerGladiator))
		{
			if (rcData.collider.gameObject != this.gameObject)
			{
				GladiatorNetwork	opo = rcData.collider.gameObject.transform.GetComponent<GladiatorNetwork>();
				
				if (opo.TeamNb != RunTimeData.PlayerTeam)
				{
					_gladiatorNetwork.SendAttack(opo, _damage);
					_haveTouched = true;
				}
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
			int				multiplier = 1;
			
			if (GladiatorNetwork._myGladiator.Class == SelectClass.eClass.LIGHT)
			{
				multiplier = 4;
				_gldCtrl.lightDodge();
			}
			
			_defenseEnergySpend += Time.deltaTime * _defenseCost * multiplier;
			intEnergySpend = (int)_defenseEnergySpend;
			_gladiatorNetwork.Energy -= intEnergySpend;
			_defenseEnergySpend -= intEnergySpend;
		}
	}
}
