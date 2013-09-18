/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public class 				CreateJoinRoom : Photon.MonoBehaviour
{
	public bool				_create;
	public GameObject		_panelRoom;
	
	void 					OnClick()
	{
		if (_create)
		{
			PhotonNetwork.CreateRoom(RunTimeData.PlayerBase.PlayerName, true, true, 6);
			PhotonNetwork.JoinRoom(RunTimeData.PlayerBase.PlayerName);
		}
		else
			PhotonNetwork.JoinRoom(transform.FindChild("GameName").GetComponent<UILabel>().text);
	}
	
	void					OnJoinedRoom()
	{
		_panelRoom.SetActive(true);
		transform.parent.gameObject.SetActive(false);
	}
}
