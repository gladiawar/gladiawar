/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public class 				BasePhoton : MonoBehaviour
{
	void					Awake()
	{
        if (!PhotonNetwork.connected)
            PhotonNetwork.ConnectUsingSettings("v1.0");
		PhotonNetwork.playerName = RunTimeData.PlayerBase.PlayerName;
	}
}
