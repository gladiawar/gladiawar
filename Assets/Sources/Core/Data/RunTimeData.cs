/*
 * 
 * 
 * 
 */

using							UnityEngine;
using							System.Collections;

public static class 			RunTimeData
{
	private static string		_sessionID;
	public static string		sessionID
	{
		get { return (_sessionID); }
		set { _sessionID = value; }
	}
	
	private static PlayerBase	_playerBase = null;
	public static PlayerBase	PlayerBase
	{
		get
		{
			if (_playerBase == null)
				_playerBase = new PlayerBase();
			return (_playerBase);
		}
	}
}
