/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public class 				AnimationStateManager : MonoBehaviour
{
	public enum 			eState
	{
		IDLE,
		WALK,
		RUN,
		BACK,
		ATTACK,
		RECEIVEATTACK,
		DEFENSE,
		DIE
	}
	
	#region REMOTE
	
	private bool			_remote = false;
	public bool				Remote
	{ set { _remote = value; } }
	
	#endregion
	
	private Animation		_animation;
	private CharacterController _charCtrl;
	
	public GameObject[]		_particle;
	public string[]			_animationNames;
	
	private eState			_state;
	public eState			State
	{
		get { return (_state); }
		set
		{
			_state = value;
			majAnimation();
		}
	}
	
	void 					Start()
	{
		_animation = transform.GetComponent<Animation>();
		_charCtrl = transform.GetComponent<CharacterController>();
		_state = eState.IDLE;
	}
	
	void					Update()
	{
		if (!_remote)
		{
			switch (_state)
			{	
			case eState.IDLE:
				IdleUpdateCycle(); break;
			case eState.ATTACK:
				WaitForAnimationPlaying("attack1"); break;
			case eState.RECEIVEATTACK:
				WaitForAnimationPlaying("hit"); break;
			case eState.WALK:
			case eState.RUN:
			case eState.BACK:
				MoveUpdateStatus(); break;
			}
		}
	}
	
	private void			WaitForAnimationPlaying(string animationName)
	{
		if (!_animation.IsPlaying(animationName))
			State = eState.IDLE;
	}
	
	private void			IdleUpdateCycle()
	{
		if (_charCtrl.velocity.sqrMagnitude > 20f)
			_state = eState.RUN;
		else if (_charCtrl.velocity.sqrMagnitude > 0.1f)
		{
			if (_state != eState.BACK)
				_state = eState.WALK;
		}
		else
			_animation.CrossFade("fight idle");
	}
	
	private void			MoveUpdateStatus()
	{
		if (_charCtrl.velocity.sqrMagnitude < 0.1)
			State = eState.IDLE;
		else if (_state == eState.BACK)
			State = eState.BACK;
		else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			State = eState.RUN;
		else
			State = eState.WALK;
	}
	
	private void			majAnimation()
	{
		if (_animation == null)
			return ;
		switch (_state)
		{
		case eState.ATTACK:
			_animation.CrossFade("attack1"); break;
		case eState.WALK:
			animation["walk"].speed = 1.0f;
			_animation.CrossFade("walk"); break;
		case eState.BACK:
			animation["walk"].speed = -1.0f;
			_animation.CrossFade("walk"); break;
		case eState.RUN:
			_animation.CrossFade("run fast"); break;
		case eState.IDLE:
			_animation.CrossFade("fight idle"); break;
		case eState.RECEIVEATTACK:
			bloodGiclure();
			_animation.Play("hit"); break; 
		case eState.DIE:
			bloodDie();
			_animation.CrossFade("die1"); break;
		case eState.DEFENSE:
			_animation.Play("gblock"); break;
		}
	}
	
	private void			bloodGiclure()
	{
		GameObject			giclure = CFX_SpawnSystem.GetNextObject(_particle[0], true);
		
		giclure.transform.position = transform.position + new Vector3(0, 0.6f, 0);
	}
	
	private void			bloodDie()
	{
		GameObject			blood = CFX_SpawnSystem.GetNextObject(_particle[1], true);
		
		blood.transform.position = transform.position;
	}
}
