using UnityEngine;
using System.Collections;

public class GTabButton : GButton_OnClick {
	
	public GTabs_Content content;
	
	public GTabs tabs {get; set;}
	
	public void Start() {
		this.content.gameObject.SetActive(false);	
	}
	
	public override void OnClick(GButton caller) {
		this.tabs.OnActive(this);
	}
	
}
