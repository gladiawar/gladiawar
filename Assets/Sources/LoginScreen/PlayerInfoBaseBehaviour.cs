/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public class 				PlayerInfoBaseBehaviour : MonoBehaviour
{
	public string 				PlayerName;
	public SelectClass.eClass	PlayerClass;
	
	public void				OnClick()
	{
		RunTimeData.PlayerBase.PlayerName = PlayerName;
		RunTimeData.PlayerBase.PlayerClass = PlayerClass;
		Application.LoadLevel("Hub");
	}
}
