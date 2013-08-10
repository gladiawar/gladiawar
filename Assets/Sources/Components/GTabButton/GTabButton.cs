using UnityEngine;
using System.Collections;

public class GTabButton : GButton_OnClick {
	
	public GTabs_Content content;
	
	public GTabs tabs { get; set; }
	
	public override void OnClick(GButton caller) {
		this.tabs.active(this);
	}
	
}
