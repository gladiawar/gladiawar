/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;
using						System.Collections.Generic;

public class 				RefreshGameList : MonoBehaviour
{
	public GameObject		_RoomButton;
	public GameObject		_grid;
	public GameObject		_panelRoom;
	
	private bool			_firstTimeDone = false;
	private List<GameObject> _roomList;
	
	void					Start()
	{
		_roomList = new List<GameObject>();
	}
	
	void					Update()
	{
		if (!_firstTimeDone && PhotonNetwork.connected)
		{
			_firstTimeDone = true;
			majRoomList();
		}
	}
	
	void					OnClick()
	{
		clearList();
		majRoomList();
	}
	
	void					majRoomList()
	{
		foreach (RoomInfo game in PhotonNetwork.GetRoomList())
		{
			if (game.open)
			{
				GameObject		prefab = NGUITools.AddChild(_grid, _RoomButton);
			
				prefab.transform.localScale = new Vector3(2, 2, 1);
				prefab.GetComponent<CreateJoinRoom>()._panelRoom = _panelRoom;
				prefab.transform.FindChild("GameName").GetComponent<UILabel>().text = game.name;
				_roomList.Add(prefab);
			}
		}
		_grid.GetComponent<UIGrid>().Reposition();
	}
	
	void					clearList()
	{
		foreach (GameObject go in _roomList)
			NGUITools.Destroy(go);
		_roomList.Clear();
	}
}
