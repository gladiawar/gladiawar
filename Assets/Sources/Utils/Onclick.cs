using UnityEngine;
using System.Collections;

public class Onclick : MonoBehaviour 
{
	public GameObject 	target;
	public string		functionName;
	
	void	OnClick()
	{
		target.SendMessage(functionName, SendMessageOptions.DontRequireReceiver);
	}
}
