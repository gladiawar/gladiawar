using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReadyList : MonoBehaviour 
{
	private Dictionary<string, bool>	userReadyness;
	public	UILabel						ModeleLabel;
	int									labelNumber = 0;
	
	void Awake()
	{
		userReadyness = new Dictionary<string, bool>();
	}
	
	// Use this for initialization
	void Start () 
	{
		//networkView.RPC("SetStatus", RPCMode.All, GameObject.Find("PlayerPrefs").GetComponent<PlayerInfo>().GetPlayerName(), false);
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	[RPC]
	public void	SetStatus(string userName, bool status)
	{
		userReadyness[userName] = status;
		UpdatePanel();
	}
	
	private void UpdatePanel()
	{
		foreach (KeyValuePair<string, bool> pair in userReadyness)
		{
			UILabel label = GetUILabelFromUsername(pair.Key);
			if (pair.Value == true)
			{
				label.color = Color.green;
			}
			else
			{
				label.color = Color.red;
			}
		}
	}
	
	private UILabel	GetUILabelFromUsername(string userName)
	{
		UILabel		label;
		Transform	tf = transform.FindChild("StatusUser_" + userName);
		
		if (tf == null)
		{
			label = NGUITools.AddWidget<UILabel>(ModeleLabel.transform.parent.gameObject);
			
			label.font = ModeleLabel.font;
			label.transform.localPosition = ModeleLabel.transform.localPosition - new Vector3(0, 25 * labelNumber, 0);
			label.transform.localScale = ModeleLabel.transform.localScale;
			label.effectStyle = ModeleLabel.effectStyle;
			label.pivot = ModeleLabel.pivot;
			label.name = "StatusUser_" + userName;
			label.text = userName;
			label.depth = ModeleLabel.depth + labelNumber;
			label.transform.gameObject.SetActive(true);
			++labelNumber;
		}
		else
			label = tf.GetComponent<UILabel>();
		return (label);
	}
}
