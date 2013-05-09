using UnityEngine;
using System.Collections;

public class OnClickSendMessage : MonoBehaviour 
{
	public GameObject 			Target;
	public string				FunctionName;
	public string				Message;
	public SendMessageOptions	MessageOption = SendMessageOptions.DontRequireReceiver;
	
	void	OnClick()
	{
		Target.SendMessage(this.FunctionName, this.Message, this.MessageOption);
	}
}
