/*
 * 
 * 
 * 
 */

using					UnityEngine;
using					System.Collections;
using					System.Collections.Generic;

public class 			DisplayPersoList : MonoBehaviour
{
	public GameObject	_panel;
	private Transform[] _slotPerso;
	
	void 				Start()
	{
		_slotPerso = new Transform[3];
		_slotPerso[0] = transform.FindChild("1");
		_slotPerso[1] = transform.FindChild("2");
		_slotPerso[2] = transform.FindChild("3");
	}
	
	public void			loadCharacters()
	{
		SDNet.Instance.NormalRequest(GamePHP.Instance.MainUrl + "listp/?PHPSESSID=" + RunTimeData.sessionID, OnCharactersLoaded, new Dictionary<string, string>() { { "u", "" } });
	}
	
	public void			OnCharactersLoaded(SDNet.ReturnCode code, string res)
	{
		if (code == SDNet.ReturnCode.OK)
		{
			Debug.Log(res);
		}
		else
			_panel.GetComponent<PanelLogin>().State = PanelLogin.ePanelLoginState.LOGIN;
	}
}
