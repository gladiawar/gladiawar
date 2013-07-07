/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public class 				HitDetector : MonoBehaviour
{
	void					OnTriggerEnter(Collider other)
	{
		Debug.Log(other.tag);
		if (other.tag.Equals("Player") && other.gameObject != gameObject)
			Debug.Log("touched");
	}
}
