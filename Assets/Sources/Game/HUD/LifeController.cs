/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public class 				LifeController : MonoBehaviour
{
	private int				life = -1;
	private float			xBaseBound;
	private UISprite		sprite;
	
	void					Awake()
	{
		xBaseBound = transform.localScale.x;
	}
	
	void					Update()
	{
		if (life != GladiatorNetwork._myGladiator.Life)
		{
			transform.localScale = new Vector3(xBaseBound * ((float)GladiatorNetwork._myGladiator.Life / 100f), transform.localScale.y, transform.localScale.z);
			life = GladiatorNetwork._myGladiator.Life;
		}
	}
}
