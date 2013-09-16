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
	public GameObject		_panelRoom;
	
	void 					OnClick()
	{
		if (_random)
		{
			PhotonNetwork.JoinRandomRoom();
		}
		else
		{
			if (_create)
				PhotonNetwork.CreateRoom(_gameName.text, true, true, 6);
			PhotonNetwork.JoinRoom(_gameName.text);
		}
	}
	
	void					OnJoinedRoom()
	{
		_panelRoom.SetActive(true);
		transform.parent.gameObject.SetActive(false);
		//PhotonNetwork.Instantiate("ReadyChecker", Vector3.zero, Quaternion.Euler(Vector3.zero), 0);
	}
}
