/*
 * 
 * 
 * 
 */

using						UnityEngine;
using 						System.Collections;

public class 				Login : MonoBehaviour
{
#region editor_var
	public Transform		_emailInput;
	public Transform		_mdpInput;
#endregion
	
#region private_functions
	void					OnClick()
	{
		StartCoroutine(SDNet.Instance.Login(OnLogin, _emailInput.GetComponent<UIInput>().text, _mdpInput.GetComponent<UIInput>().text));
	}
	
	private void			OnLogin(SDNet.ReturnCode code, string res)
	{
		if (code == SDNet.ReturnCode.OK)
		{
			Debug.Log("Connect");
		}
		else
		{
			Debug.Log("unconnect");
		}
	}
#endregion
}
