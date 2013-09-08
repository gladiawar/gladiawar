using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Basic class controller of GUserInputSetterBox
 * 
 * @prefab GUserInputSetterBox
 */
public class GuserInputSetterBox : MonoBehaviour {
	
	protected GUserInputSetterHandler handler;
	protected GMenu menu;
	
	void Start() {
		this.menu = this.GetComponent<GMenu>();
	}
	
	void OnGUI() {
		Event ev = Event.current;
		
		if (this.handler != null && (ev.isKey || ev.isMouse)) {
			this.handler.OnCaptureKey(ev.keyCode);
		}
	}
	
	public void setHandlerAndShow(GUserInputSetterHandler handler) {
		this.handler = handler;
		this.menu.show();
	}
	
	public void unsetHandlerAndHide() {
		this.handler = null;
		this.menu.hide();
	}
	
}
