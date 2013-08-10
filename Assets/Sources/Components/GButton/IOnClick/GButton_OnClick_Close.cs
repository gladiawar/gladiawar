using UnityEngine;
using System.Collections;

public class GButton_OnClick_Close : GButton_OnClick {
	
	public GameObject close;
	
	public override void OnClick(GButton caller) {
		this.close.SetActive(false);
	}
	
}
