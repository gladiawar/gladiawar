/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public class 				LifeController : MonoBehaviour
{
	private int				_life = -1;
	private float			_xBaseBound;
	
	void					Awake()
	{
		_xBaseBound = transform.localScale.x;
	}
	
	void					Update()
	{
		if (_life != GladiatorNetwork._myGladiator.Life)
		{
			transform.localScale = new Vector3(_xBaseBound * ((float)GladiatorNetwork._myGladiator.Life / 100f), transform.localScale.y, transform.localScale.z);
			_life = GladiatorNetwork._myGladiator.Life;
		}
	}
}
