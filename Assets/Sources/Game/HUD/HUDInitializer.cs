/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;
using						System.Collections.Generic;

public class 				HUDInitializer : MonoBehaviour
{
	public UILabel			_name;
	public List<GameObject> _hud;
	
	private static HUDInitializer _instance;
	public static HUDInitializer Instance
	{ get { return (_instance); } }
	
	
	void 					Start()
	{
		_instance = this;
		foreach (GameObject go in _hud)
			go.SetActive(false);
	}
	
	public void				init(GladiatorNetwork gladiator)
	{
		foreach (GameObject go in _hud)
			go.SetActive(true);
		_name.text = RunTimeData.PlayerBase.PlayerName;
		_name.color = (RunTimeData.PlayerTeam == 0 ? Color.blue : Color.red);
	}
}
