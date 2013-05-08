using UnityEngine;
using System.Collections;

public class GameChat : MonoBehaviour 
{
	private UITextList	chatBox;
	
	// Use this for initialization
	void Start () 
	{
		chatBox = GetComponent<UITextList>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnSubmitChatText(string msg)
	{
		networkView.RPC("SubmitChatText", RPCMode.All, msg);
	}
	
	[RPC]
	void SubmitChatText(string msg)
	{
		chatBox.Add(msg);
	}
}
