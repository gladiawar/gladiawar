/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public abstract class		ALaunchEvent : GladiawarBehaviour
{
	public abstract void	allPlayerInstantiated();
	public abstract void	launchCountDown();
	public abstract void	launchGame();
}
