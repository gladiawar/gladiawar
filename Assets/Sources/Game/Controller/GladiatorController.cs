/*
 * 
 * 
 *
 */

using						UnityEngine;
using						System.Collections;

public class 				GladiatorController : MonoBehaviour
{
	public float			_gravity;
	public float			_walkSpeed;
	public float			_factorRun;
	public float			_acceleration;
	public float			_sensibility;
	
	const float				_runningCost = 8f;
	
	private AnimationStateManager _ASM;
	private float			_verticalSpeed = 0.0f;
	private CharacterController _charCtrl;
	private float			_moveSpeed = 0.0f;
	private Vector3			_vForward;
	
	void					Start()
	{
		_charCtrl = transform.GetComponent<CharacterController>();
		_ASM = transform.GetComponent<AnimationStateManager>();
		Screen.showCursor = false;
	}
	
	void					Update()
	{
		applyGravity();
		changeRotation();
		updateMovement();
	}
	
	void					updateMovement()
	{
		float				targetSpeed = 0;
		
		if (Input.GetKey(Keyboard.Action_Forward) ^ Input.GetKey(Keyboard.Action_Back))
		{
			bool			back = Input.GetKey(Keyboard.Action_Back);
			
			if (Input.GetKey(Keyboard.Action_Left) && !Input.GetKey(Keyboard.Action_Right))
				_vForward = new Vector3((back ? 0.5f : -0.5f), 0, 0.5f);
			else if (!Input.GetKey(Keyboard.Action_Left) && Input.GetKey(Keyboard.Action_Right))
				_vForward = new Vector3((back ? -0.5f : 0.5f), 0, 0.5f);
			else
				_vForward = Vector3.forward;
			moveForward(ref targetSpeed, back, Input.GetKey(Keyboard.Action_Run));
			if (back)
				_ASM.State = AnimationStateManager.eState.BACK;
		}
		else if (Input.GetKey(Keyboard.Action_Left) ^ Input.GetKey(Keyboard.Action_Right))
		{
			_vForward = (Input.GetKey(Keyboard.Action_Left) ? Vector3.left : Vector3.right);
			moveForward(ref targetSpeed);
		}
		_moveSpeed = Mathf.Lerp(_moveSpeed, targetSpeed, _acceleration);
		applyMovement();
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
	
	void					applyMovement()
	{
		float			currentYRotation = transform.eulerAngles.y;
		Quaternion		RotationY = Quaternion.Euler(0, currentYRotation, 0);

		_charCtrl.Move((RotationY * _vForward * Time.deltaTime * _moveSpeed) + new Vector3(0, _verticalSpeed, 0));
	}
	
	void					moveForward(ref float targetSpeed, bool back = false, bool running = false)
	{
		targetSpeed = _walkSpeed;
		if (back)
			targetSpeed *= -1;
		else if (running && GladiatorNetwork._myGladiator.isRunning(Time.deltaTime * _runningCost))
		{
			targetSpeed *= _factorRun;
		}
	}
}
