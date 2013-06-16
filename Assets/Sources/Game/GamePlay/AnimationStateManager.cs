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
		ATTACK
	}
	
	private Animation		_animation;
	private CharacterController _charCtrl;
	
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
		switch (_state)
		{	
		case eState.IDLE:
			IdleUpdateCycle(); break;
		case eState.ATTACK:
			AttackUpdateCycle(); break;
		case eState.WALK:
		case eState.RUN:
			MoveUpdateStatus(); break;
		}
	}
	
	private void			AttackUpdateCycle()
	{
		if (!_animation.IsPlaying("attack1"))
			_state = eState.IDLE;
	}
	
	private void			IdleUpdateCycle()
	{
		if (_charCtrl.velocity.sqrMagnitude > 0.1)
			_state = ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? eState.RUN : eState.WALK);
		else
			_animation.CrossFade("idle");
	}
	
	private void			MoveUpdateStatus()
	{
		if (_charCtrl.velocity.sqrMagnitude < 0.1)
			State = eState.IDLE;
		else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			State = eState.RUN;
		else
			State = eState.WALK;
	}
	
	private void			majAnimation()
	{
		switch (_state)
		{
		case eState.ATTACK:
			_animation.CrossFade("attack1"); break;
		case eState.WALK:
			_animation.CrossFade("walk"); break;
		case eState.RUN:
			_animation.CrossFade("run fast"); break;
		}
	}
}
