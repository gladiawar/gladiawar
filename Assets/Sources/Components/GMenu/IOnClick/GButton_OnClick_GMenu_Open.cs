using UnityEngine;
using System.Collections;

public class GButton_OnClick_GMenu_Open : GButton_OnClick {
	
	public GMenu menu;
	
	public override void OnClick(GButton caller) {
		this.menu.show();
	}
	
}
