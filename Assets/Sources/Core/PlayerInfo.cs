using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour 
{
	public bool					useDontDestroyOnLoad = false;
	
	public bool					PlayerIsSet = false;
	public string 				PlayerName;
	public SelectClass.eClass	PlayerClass;
	
	
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
	
	void	OnLevelWasLoaded()
	{
		GameObject go = GameObject.FindWithTag("EditorOnly");
		
		if (transform.gameObject != go)
			DestroyObject(go);
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
