using UnityEngine;
using System.Collections;

public class GButton_OnClick_Open : GButton_OnClick {
	
	public GameObject open;
	
	public override void OnClick(GButton caller) {
		this.open.SetActive(true);
	}
	
}
