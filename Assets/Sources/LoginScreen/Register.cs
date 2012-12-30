/*
 * 
 * 
 * 
 */

using 						UnityEngine;
using 						System.Collections;

public class 				Register : MonoBehaviour
{
#region editor_var
	
	public Transform		_emailInput;
	public Transform		_mdpInput;
	public Transform		_mdp2Input;
	
#endregion
	
#region private_functions

	void					OnClick()
	{
		if (_mdpInput.GetComponent<UIInput>().text.Equals(_mdp2Input.GetComponent<UIInput>().text))
			StartCoroutine(SDNet.Instance.Register(OnRegister, _emailInput.GetComponent<UIInput>().text, _mdpInput.GetComponent<UIInput>().text, _emailInput.GetComponent<UIInput>().text));
	}
	
	private void			OnRegister(SDNet.ReturnCode code, string res)
	{
		if (code == SDNet.ReturnCode.OK)
		{
			Debug.Log("Register !");
		}
		else
		{
			Debug.Log("unregister");
		}
	}
	
#endregion
}
