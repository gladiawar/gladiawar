using UnityEngine;
using System.Collections;

public class GPauseMenu : GMenu {
	
	public override void init(GMenuManager manager) {
		base.init(manager);
		manager.DefaultMenu = this;
	}
	
}
