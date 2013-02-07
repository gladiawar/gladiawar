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
	public Transform		_panel;	
	
#endregion
	
#region private_functions

	void					OnClick()
	{
		if (_mdpInput.GetComponent<UIInput>().text.Equals(_mdp2Input.GetComponent<UIInput>().text))
			StartCoroutine(SDNet.Instance.Register(OnRegister, _emailInput.GetComponent<UIInput>().text, _mdpInput.GetComponent<UIInput>().text, _emailInput.GetComponent<UIInput>().text.Substring(0, 5)));
	}
	
	private void			OnRegister(SDNet.ReturnCode code, string res)
	{
		if (code == SDNet.ReturnCode.OK)
			_panel.GetComponent<PanelLogin>().State = PanelLogin.ePanelLoginState.LOGIN;
		else
			ErrorMenu.Instance.setErrorMsg(res);
	}
	
#endregion
}
