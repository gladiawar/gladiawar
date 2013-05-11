using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour 
{
	// Parce qu'on stock ça à l'arrache dans un script de l'écran de login
	public bool					useDontDestroyOnLoad = false;
	
	// Parce qu'un script a besoin de savoir si playerinfo a été rempli
	public bool					PlayerIsSet = false;
	
	// Une variable statique pour accèder directement à ce truc dès qu'il existe.
	public static PlayerInfo	playerInfo = null;	
	
	public string 				PlayerName;
	public SelectClass.eClass	PlayerClass;
	
	
	void	Awake()
	{
		if (useDontDestroyOnLoad && PlayerInfo.playerInfo == null)
			PlayerInfo.playerInfo = this;
	}
	
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
	
	// Vire les doublons (de l'éditeur), pour éviter de se taper l'écran de login
	void	OnLevelWasLoaded()
	{
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("EditorOnly"))
		{
			if (go.name == "PlayerPrefs" && go != playerInfo)
				Destroy(go);
		}
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
