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
		GameObject			myPlayer = PhotonNetwork.Instantiate("NormalPlayer", new Vector3(11, 0.59f, -25), Quaternion.Euler(new Vector3(0, 0, 0)), 0);
		PhotonView			pv;
		
		myPlayer.GetComponent<ThirdPersonCamera>().cameraTransform = Camera.mainCamera.transform;
		pv = myPlayer.GetComponent<PhotonView>();
		pv.ownerId = Random.Range(0, 500);
		pv.observed = myPlayer.transform;
	}
}
