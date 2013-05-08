using UnityEngine;
using System.Collections;

public class GameButtonReady : MonoBehaviour 
{
	public bool			Status = false;
	public ReadyList	readylist;
	private UILabel		label;
	public	NetworkView	netview;
	
	void	Awake()
	{
		label = GetComponentInChildren<UILabel>();
	}
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void	OnClick()
	{
		Status = !(Status);
		
		if (Status)
		{
			netview.RPC("SetStatus", RPCMode.All, GameObject.Find("PlayerPrefs").GetComponent<PlayerInfo>().GetPlayerName(), true);
			label.text = "Ready";
		}
		else
		{
			netview.RPC("SetStatus", RPCMode.All, GameObject.Find("PlayerPrefs").GetComponent<PlayerInfo>().GetPlayerName(), false);
			label.text = "Not Ready";
		}
	}
}
