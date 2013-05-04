using UnityEngine;
using System.Collections;

public class Onclick : MonoBehaviour 
{
	public GameObject 			target;
	public string				functionName;
	public SendMessageOptions	MessageOption = SendMessageOptions.DontRequireReceiver;
	
	void	OnClick()
	{
		target.SendMessage(functionName, this.MessageOption);
	}
}
