/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public class 				ErrorMenu : MonoBehaviour
{
	private UILabel			_text;
	private static ErrorMenu _instance;
	
	void 					Start()
	{
		_text = transform.GetComponent<UILabel>();
		_instance = this;
	}
	
	public static ErrorMenu Instance
	{ get { return (_instance); } }
	
	public void				reset()
	{
		_text.text = "";
	}
	
	public void				setErrorMsg(string msg)
	{
		_text.text = msg;
	}
}
