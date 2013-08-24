using UnityEngine;
using System.Collections;

public class GButton : GladiawarBehaviour {
	
	protected GButton_OnClick[] callbacks;
	
	void Start () {
		this.callbacks = this.transform.GetComponents<GButton_OnClick>();
	}
	
	public void OnClick() {
		foreach(var callback in this.callbacks) {
			callback.OnClick(this);	
		}
	}
	
}
