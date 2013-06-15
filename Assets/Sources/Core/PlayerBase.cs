/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public class 				PlayerBase
{
	private string			_playerName;
	private SelectClass.eClass _playerClass;
	
	public string			PlayerName
	{
		get { return (_playerName); }
		set { _playerName = value; }
	}
	
	public SelectClass.eClass PlayerClass
	{
		get { return (_playerClass); }
		set { _playerClass = value; }
	}
}
