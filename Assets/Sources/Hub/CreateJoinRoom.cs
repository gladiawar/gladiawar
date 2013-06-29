/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public class 				CreateJoinRoom : MonoBehaviour
{
	public UILabel			_gameName;
	public bool				_create;
	public bool				_random;
	
	void 					OnClick()
	{
		if (_random)
		{
			//PhotonNetwork.JoinRandomRoom();
		}
		else
		{
			if (_create)
				PhotonNetwork.CreateRoom(_gameName.text, true, true, 6);
			PhotonNetwork.JoinRoom(_gameName.text);
			Application.LoadLevel("GamePlayDevScene1");
		}
	}
}
