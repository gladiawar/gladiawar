/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public class 				HitDetector : MonoBehaviour
{
	public GameObject		_me;
	
	void					OnTriggerEnter(Collider other)
	{
		Debug.Log(other.tag);
		if (other.tag.Equals("Player") && other.gameObject != _me)
			Debug.Log("touched");
	}
}
