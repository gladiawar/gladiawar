using UnityEngine;
using System.Collections;

/**
 * Basic implementation for show GMenu on OnClick of GButton
 * 
 * @prefab GMenu
 * @author Claude Ramseyer
 */
public class GButton_OnClick_GMenu_Open : GButton_OnClick {
	
	//Unity attributes
	public GMenu menu;
	
	//Functions
	public override void OnClick(GButton caller) {
		this.menu.show();
	}
	
}
