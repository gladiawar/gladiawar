/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public class 				PanelLogin : MonoBehaviour
{
	public enum 			ePanelLoginState
	{
		LOGIN,
		REGISTER,
		CHARA
	}
	
	public GameObject		_panelLogin;
	public GameObject		_panelRegister;
	public GameObject		_panelChar;
	
	private ePanelLoginState _state = ePanelLoginState.LOGIN;
	private static PanelLogin _instance = null;
	
	public static PanelLogin Instance
	{ get { return (_instance); } }
	
	public ePanelLoginState State
	{
		set
		{
			_state = value;
			majPanel();
		}
	}
	
	void 					Start()
	{
		majPanel();
		_instance = this;
	}
	
	private void			majPanel()
	{
		switch (_state)
		{
		case ePanelLoginState.LOGIN:
			activePanels(true, false, false); break;
		case ePanelLoginState.REGISTER:
			activePanels(false, true, false); break;
		case ePanelLoginState.CHARA:
			activePanels(false, false, true); break;
		}
	}
	
	private void			activePanels(bool login, bool register, bool chara)
	{
		_panelLogin.SetActive(login);
		_panelRegister.SetActive(register);
		_panelChar.SetActive(chara);
	}
	
}
