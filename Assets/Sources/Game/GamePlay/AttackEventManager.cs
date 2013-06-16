/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public class 				AttackEventManager : MonoBehaviour
{
	private AnimationStateManager _animMngr;
	
	void 					Start()
	{
		_animMngr = transform.GetComponent<AnimationStateManager>();
	}
	
	void 					Update()
	{
		if (Input.GetMouseButtonDown(0))
			_animMngr.State = AnimationStateManager.eState.ATTACK;
	}
}
