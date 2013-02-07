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
	public Transform		_panel;
#endregion
	
#region private_functions
	void					OnClick()
	{
		StartCoroutine(SDNet.Instance.Login(OnLogin, _emailInput.GetComponent<UIInput>().text, _mdpInput.GetComponent<UIInput>().text));
	}
	
	private void			OnLogin(SDNet.ReturnCode code, string res)
	{
		if (code == SDNet.ReturnCode.OK)
			_panel.GetComponent<PanelLogin>().State = PanelLogin.ePanelLoginState.CHARA;
		else
			ErrorMenu.Instance.setErrorMsg(res);
	}
#endregion
}
