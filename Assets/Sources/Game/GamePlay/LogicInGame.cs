/*
 * 
 * 
 * 
 *
 */

using						UnityEngine;
using						System.Collections;

public class 				LogicInGame : MonoBehaviour
{
	void					Start()
	{
		GameObject			myPlayer = PhotonNetwork.Instantiate("NormalPlayer", new Vector3(11, 0.65f, -25), Quaternion.Euler(new Vector3(0, 0, 0)), 0);
		PhotonView			pv;
		
		pv = myPlayer.GetComponent<PhotonView>();
		pv.ownerId = PhotonNetwork.player.ID;
	}
}
