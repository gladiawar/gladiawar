using UnityEngine;
using System.Collections;

public class SimpleAttack : MonoBehaviour {
	
	public Animation _animAttack;
	
	protected virtual void Update() 
	{
		// Note : Garder ça jusqu'a l'autre chose en dessous soit op.
		// Ca permet de tester le réseau
		if (Input.GetMouseButtonDown (0))
		{
		RaycastHit	hitinfo = new RaycastHit();
		
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitinfo, 3))
			{
			print("I hitted him !");
			if (hitinfo.transform.CompareTag("Player"))
				networkView.RPC("PlayerIsHitted", RPCMode.All, hitinfo.transform.networkView.viewID);
			}
		}

		if (Input.GetMouseButtonDown(0) && networkView.isMine)
			if (!_animAttack.isPlaying)
				_animAttack.Play();
	}
}
