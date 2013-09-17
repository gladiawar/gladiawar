/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;
using						System.Collections.Generic;

public class 				ChangeMap : MonoBehaviour
{
	public List<string>		_mapName;
	private int				_currentIndex = 0;
	
	void					Start()
	{
		RunTimeData.MapName = _mapName[_currentIndex];
	}
	
	void					OnClick()
	{
		if (++_currentIndex == _mapName.Count)
			_currentIndex = 0;
		RunTimeData.MapName = _mapName[_currentIndex];
	}
}
