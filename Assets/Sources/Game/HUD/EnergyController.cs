/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public class 				EnergyController : MonoBehaviour
{
	private int				_energy = -1;
	private float			_xBaseBound;
	
	void					Awake()
	{
		_xBaseBound = transform.localScale.x;
	}
	
	void					Update()
	{
		if (_energy != GladiatorNetwork._myGladiator.Energy)
		{
			transform.localScale = new Vector3(_xBaseBound * ((float)GladiatorNetwork._myGladiator.Energy / 100f), transform.localScale.y, transform.localScale.z);
			_energy = GladiatorNetwork._myGladiator.Energy;
		}
	}
}
