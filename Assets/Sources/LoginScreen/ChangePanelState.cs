/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public class 				ChangePanelState : MonoBehaviour
{
	public PanelLogin.ePanelLoginState _state;
	public Transform		_panel;
	
	void					OnClick()
	{
		_panel.GetComponent<PanelLogin>().State = _state;
	}
}
