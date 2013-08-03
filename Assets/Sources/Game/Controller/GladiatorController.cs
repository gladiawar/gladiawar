/*
 * 
 * 
 *
 */

using						UnityEngine;
using						System.Collections;

public class 				GladiatorController : MonoBehaviour
{
	public enum 			eCharState
	{
		IDLE,
		WALKING,
		RUNNING,
		STRAFELEFT,
		STRAFERIGHT,
		BACK
	}
	
	public float			_gravity;
	public float			_walkSpeed;
	public float			_factorRun;
	public float			_acceleration;
	public float			_sensibility;
	
	private eCharState		_characterState;
	private float			_verticalSpeed = 0.0f;
	private CharacterController _charCtrl;
	private float			_moveSpeed = 0.0f;
	
	void					Start()
	{
		_charCtrl = transform.GetComponent<CharacterController>();
		_characterState = eCharState.IDLE;
		Screen.showCursor = false;
	}
	
	void 					Update()
	{
		applyGravity();
		changeRotation();
		updateMovement();
	}
	
	void					applyGravity()
	{
		_verticalSpeed -= _gravity * Time.deltaTime;
	}
	
	void					changeRotation()
	{
		float				rotation = (Input.GetAxis("Mouse X") * _sensibility) * Time.deltaTime;
		
		transform.Rotate(0, rotation, 0);
	}
	
	void					updateMovement()
	{
		float				targetSpeed = 0;
		
		switch (_characterState)
		{
		case eCharState.IDLE:
			updateMovementFromIdle(); break;
		case eCharState.WALKING:
		case eCharState.RUNNING:
			updateMovementFromForward(); break;
		case eCharState.BACK:
			if (Input.GetKeyUp(KeyCode.S)) _characterState = eCharState.IDLE; break;
		}
		if (_characterState == eCharState.WALKING || _characterState == eCharState.RUNNING || _characterState == eCharState.BACK)
			moveForward(ref targetSpeed);
		_moveSpeed = Mathf.Lerp(_moveSpeed, targetSpeed, _acceleration);
		applyMovement();
	}
	
	void					applyMovement()
	{
		float			currentYRotation = transform.eulerAngles.y;
		Quaternion		RotationY = Quaternion.Euler(0, currentYRotation, 0);
			
		_charCtrl.Move((RotationY * Vector3.forward * Time.deltaTime * _moveSpeed) + new Vector3(0, _verticalSpeed, 0));
	}
	
	void					updateMovementFromIdle()
	{
		if (Input.GetKeyDown(KeyCode.W))
		{
			_characterState = eCharState.WALKING;
			if (Input.GetKeyDown(KeyCode.LeftShift))
				_characterState = eCharState.RUNNING;
		}
		else if (Input.GetKeyDown(KeyCode.S))
			_characterState = eCharState.BACK;
	}
	
	void					updateMovementFromForward()
	{
		if (_characterState == eCharState.RUNNING)
		{
			if (Input.GetKeyUp(KeyCode.LeftShift))
				_characterState = eCharState.WALKING;
		}
		else if (Input.GetKeyDown(KeyCode.LeftShift))
			_characterState = eCharState.RUNNING;
		if (Input.GetKeyUp(KeyCode.W))
			_characterState = eCharState.IDLE;
	}
	
	void					moveForward(ref float targetSpeed)
	{
		targetSpeed = _walkSpeed;
		if (_characterState == eCharState.BACK)
			targetSpeed *= -1;
		else if (_characterState == eCharState.RUNNING)
			targetSpeed *= _factorRun;
	}
}
