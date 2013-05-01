using UnityEngine;
using System.Collections;

public class ShowPlayerLife : MonoBehaviour 
{
	public Texture	Life_Ok;
	public Texture	Life_Loss;
	public Vector2	size;
	public Object[]	OthersPl;
	
	// Use this for initialization
	void Start () 
	{
		size.x = 150;
		size.y = 25;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnGUI ()
	{
		int i = 0;
		OthersPl = PlayerStat.FindObjectsOfType(typeof(PlayerStat));
		
		GUI.BeginGroup(new Rect(Screen.width - size.x, 0, Screen.width, OthersPl.Length * size.y + OthersPl.Length));
		foreach (PlayerStat pl in OthersPl)
		{
			DrawLifeForPlayer(pl, i);
			i++;
		}
		GUI.EndGroup();
	}
	
	void	DrawLifeForPlayer(PlayerStat pl, int number)
	{
		int	offset = (int)(number * size.y);
		if (number > 0)
			offset++;
		
		if (pl.Health > 0)
		{
			int	sizeOk = (int)((float)(pl.Health) / 100 * size.x);
			
			GUI.DrawTexture(new Rect(0, offset, sizeOk, size.y), Life_Ok);
			GUI.DrawTexture(new Rect(sizeOk, offset, size.x, size.y), Life_Loss);
		}
		else
		{
			GUI.DrawTexture(new Rect(0, offset, size.x, size.y), Life_Loss);
		}
	}
}
