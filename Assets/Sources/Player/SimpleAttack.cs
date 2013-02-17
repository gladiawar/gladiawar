using UnityEngine;
using System.Collections;

public class SimpleAttack : MonoBehaviour {
	
	public Animation _animAttack;
	
	protected virtual void Update() 
	{
		if (Input.GetMouseButtonDown(0) && networkView.isMine)
			if (!_animAttack.isPlaying)
				_animAttack.Play();
	}
}
