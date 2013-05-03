using UnityEngine;
using System.Collections;

public class ShowString : MonoBehaviour 
{
	public Rect Position;
	public string Text;
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	void	OnGUI()	 
	{
		GUI.TextField(Position, Text);
	}
}
