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
	
	private static int			_playerTeam;
	public static int			PlayerTeam
	{
		get { return (_playerTeam); }
		set { _playerTeam = value; }
	}
	
	private static int			_playerSlot;
	public static int			PlayerSlot
	{
		get { return (_playerSlot); }
		set { _playerSlot = value; }
	}
	
	private static string		_mapName;
	public static string		MapName
	{
		get { return (_mapName); }
		set { _mapName = value; }
	}
}
