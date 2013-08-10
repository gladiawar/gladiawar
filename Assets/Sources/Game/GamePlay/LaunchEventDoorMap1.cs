/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public class 				LaunchEventDoorMap1 : ALaunchEvent
{
	public float			_yTarget;
	public float			_YPS;
	
	private bool			_gameLaunched = false;
	
	public override void	allPlayerInstantiated()
	{
	}
	
	public override void	launchCountDown()
	{
	}
	
	public override void	launchGame()
	{
		_gameLaunched = true;
	}
	
	void					Update()
	{
		if (_gameLaunched)
		{
			transform.position -= new Vector3(0, _YPS * Time.deltaTime, 0);
			if (transform.position.y <= _yTarget)
				GameObject.Destroy(gameObject);
		}
	}
}
