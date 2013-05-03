using UnityEngine;
using System.Collections;

public class OnClickJoinGame : MonoBehaviour 
{
	public ListePartie_Data	data;
	
	public string	gameIp;
	public int 	gamePort = 25000;
	
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
		label.text = data.gameIp;
		label.color = Color.red;
	}
}
