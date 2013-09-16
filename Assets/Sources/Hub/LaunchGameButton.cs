/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public class 				LaunchGameButton : MonoBehaviour
{
	void 					OnEnable()
	{
		if (!PhotonNetwork.isMasterClient)
			GameObject.Destroy(gameObject);
		else
			PhotonNetwork.Instantiate("RoomLogic", Vector3.zero, Quaternion.Euler(Vector3.zero), 0);
	}
	
	void					OnClick()
	{
		PhotonNetwork.Instantiate("ReadyChecker", Vector3.zero, Quaternion.Euler(Vector3.zero), 0);
	}
}
