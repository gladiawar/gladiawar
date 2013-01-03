/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public static class 		RunTimeData
{
	private static string	_sessionID;
	
	public static string	sessionID
	{
		get { return (_sessionID); }
		set { _sessionID = value; }
	}
}
