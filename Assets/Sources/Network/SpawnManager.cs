/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;
using						System.Collections.Generic;

public class 				SpawnManager : MonoBehaviour
{
	private List<Spawn>		_spawnList;
	public List<Spawn>		SpawnList
	{
		get { return (_spawnList); }
		set { _spawnList = value ; }
	}
	
	private static SpawnManager _instance;
	public static SpawnManager Instance
	{ get { return (_instance); } }
	
	void					Awake()
	{
		_instance = this;
		_spawnList = new List<Spawn>();
	}
}
