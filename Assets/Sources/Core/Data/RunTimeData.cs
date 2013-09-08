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
			{
				_playerBase = new PlayerBase();
				_playerBase.PlayerName = "testplayer" + Random.Range(0, 100).ToString();
			}
			return (_playerBase);
		}
	}
	
	private static bool			_inIGMenu = false;
	public static bool			InIGMenu
	{
		get { return (_inIGMenu); }
		set
		{
			_inIGMenu = value;
			Screen.showCursor = _inIGMenu;
		}
	}
}
