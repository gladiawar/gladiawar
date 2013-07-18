/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public class 				Spawn : MonoBehaviour
{
	public bool				master;
	
	void 					Start()
	{
		SpawnManager.Instance.SpawnList.Add(this);
	}
}
