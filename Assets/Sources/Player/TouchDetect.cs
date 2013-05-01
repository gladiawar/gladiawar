using UnityEngine;
using System.Collections;

public class TouchDetect : MonoBehaviour
{
	void OnCollisionEnter(Collision col)
	{
		Debug.Log("Touched");
	}
}
