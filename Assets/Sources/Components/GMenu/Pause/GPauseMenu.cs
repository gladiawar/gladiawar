using UnityEngine;
using System.Collections;

/**
 * Class controller of GPauseMenu
 * 
 * @prefab GMenu
 * @prefab GPauseMenu
 * @author Claude Ramseyer
 */
public class GPauseMenu : GMenu {
	
	//Functions
	public override void init(GMenuManager manager) {
		//Set default menu
		base.init(manager);
		manager.DefaultMenu = this;
	}
	
}
