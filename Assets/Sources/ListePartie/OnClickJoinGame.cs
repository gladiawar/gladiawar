using UnityEngine;
using System.Collections;

public class OnClickJoinGame : MonoBehaviour 
{
	public GamesManagerData	data;
	
	public string[]			gameIp;
	public int 				gamePort = 25000;
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{	
	}
	
	void	OnClickSelectServer()
	{
		data.gameIp = this.gameIp;		
		data.gamePort = this.gamePort;
		
		UILabel label = GameObject.Find("InputIP").GetComponentInChildren<UILabel>();
		label.text = data.gameIp[0];
		label.color = Color.cyan;
	}
}
