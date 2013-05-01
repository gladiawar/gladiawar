using UnityEngine;
using System.Collections;

public class PlayerStat : MonoBehaviour {
	
	public int 	Health = 100;
	public bool	IsRecentlyHitted = false;
	private float TimeBeforeNormalState = 0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (IsRecentlyHitted && Time.time > TimeBeforeNormalState && Health > 0)
		{
			IsRecentlyHitted = false;
			ChangeColor(Color.green);
		}
	}
	
	[RPC]
	public void PlayerIsHitted(NetworkViewID OtherNVID)
	{
		try
		{
			PlayerStat	target = NetworkView.Find(OtherNVID).transform.GetComponentInChildren<PlayerStat>();
			
			if (target.IsRecentlyHitted != true)
			{
				target.IsRecentlyHitted = true;
				target.Health -= 5;
				target.TimeBeforeNormalState = Time.time + 2f;
				target.ChangeColor(Color.red);
			}
		}
		
		catch (System.InvalidCastException)
		{
			string message = "RPC 'PlayerIsHitted' : Exception catched (" + OtherNVID + ")";
			Debug.LogError(message);
		}
	}
	
	void OnDeath ()
	{
		print("I'm Dead !" + Health);
		this.transform.Rotate(90f, 0f, 0f);
		this.ChangeColor(Color.red);
	}
	
	protected void ChangeColor (Color newColor)
	{
		this.transform.GetComponentInChildren<MeshRenderer>().renderer.material.color = newColor;
	}
}
