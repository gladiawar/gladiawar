using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour 
{
	public bool					useDontDestroyOnLoad = false;

	private string 				PlayerName;
	private SelectClass.eClass	PlayerClass;
	
	
	// Use this for initialization
	void Start () 
	{
		if (useDontDestroyOnLoad)
			DontDestroyOnLoad(transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	public void SetPlayerName(string newName)
	{
		PlayerName = newName;
	}
	
	public void SetPlayerClass(SelectClass.eClass newClass)
	{
		PlayerClass = newClass;
	}
	
	public string GetPlayerName()
	{
		return (PlayerName);
	}
	
	public SelectClass.eClass GetPlayerClass()
	{
		return (PlayerClass);
	}
}
