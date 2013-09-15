/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public class 				CreateJoinRoom : Photon.MonoBehaviour
{
	public UILabel			_gameName;
	public bool				_create;
	public bool				_random;
	
	private	bool			_created = false;
	
	void 					OnClick()
	{
		if (_random)
		{
			//PhotonNetwork.JoinRandomRoom();
		}
		else
		{
			if (_create)
			{
				if (_created)
					CreateChecker();
				else
				{
					PhotonNetwork.CreateRoom(_gameName.text, true, true, 6);
					_created = true;
					PhotonNetwork.JoinRoom(_gameName.text);
				}
			}
			else
				PhotonNetwork.JoinRoom(_gameName.text);
		}
	}
	
	void					CreateChecker()
	{
		PhotonNetwork.Instantiate("ReadyChecker", Vector3.zero, Quaternion.Euler(Vector3.zero), 0);
	}
}
