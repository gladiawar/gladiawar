/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;
using						System.Collections.Generic;

public class 				CreateCharacter : MonoBehaviour
{
	private SelectClass.eClass _classChoose = SelectClass.eClass.LIGHT;
	public Transform		_panel;
	public Transform		_inputName;
	
	public SelectClass.eClass ClassChoosen
	{ 
		set { _classChoose = value; }
	}
	
	void					OnClick()
	{
		if (_inputName.GetComponent<UIInput>().text.Length > 2)
			SDNet.Instance.NormalRequest(GamePHP.Instance.MainUrl + "createp/?PHPSESSID=" + RunTimeData.sessionID, OnCreate,
											new Dictionary<string, string>() { { "name", _inputName.GetComponent<UIInput>().text },
																				{ "class", ((uint)_classChoose).ToString() } });
	}
	
	private void			OnCreate(SDNet.ReturnCode code, string res)
	{
		if (code == SDNet.ReturnCode.OK)
			_panel.GetComponent<PanelLogin>().State = PanelLogin.ePanelLoginState.CHARA;
		else
			Debug.Log(res);
	}
}
