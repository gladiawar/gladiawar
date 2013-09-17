/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public class 				LaunchGameButton : MonoBehaviour
{
	public GameObject		_mapPanel;
	
	void 					OnEnable()
	{
		if (!PhotonNetwork.isMasterClient)
		{
			transform.GetComponent<UIButton>().isEnabled = false;
			_mapPanel.GetComponent<UIButton>().isEnabled = false;
		}
		else
			PhotonNetwork.Instantiate("RoomLogic", Vector3.zero, Quaternion.Euler(Vector3.zero), 0);
	}
	
	void					OnClick()
	{
		PhotonNetwork.Instantiate("ReadyChecker", Vector3.zero, Quaternion.Euler(Vector3.zero), 0);
	}
}
