using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GMenuManager : GladiawarBehaviour {
	
	protected GMenu[] menus;
	protected List<GMenu> showed;
	
	public GMenu DefaultMenu { get; set; }
	public GMenu Current {
		get {
			int count = this.showed.Count;
			if (count > 0) {
				return this.showed[count - 1];
			}
			return null;
		}
	}
	
	public void Start() {
		this.menus = this.GetComponentsInChildren<GMenu>();
		this.showed = new List<GMenu>();
		
		foreach(var menu in menus) {
			menu.preShow += this.showing;
			menu.postHide += this.hidden;
			menu.init(this);
		}
	}
	
	public void Update() {
		if (Input.GetKeyDown(Keyboard.Escape)) {
			Debug.Log("ESCAPE : " + this.GetType().Name);
			GMenu current = this.Current;
			if (current == null) {
				if (this.DefaultMenu != null) {
					this.DefaultMenu.show();
				}
			}
			else {
				current.hide();
			}
		}
	}
	
	public void showing(GMenu menu) {
		GMenu current = this.Current;
		if (current != null) {
			current.background();
		}
		
		this.showed.Add(menu);
	}
	
	public void hidden(GMenu menu) {
		GMenu oldCurrent = this.Current;
		if (oldCurrent != menu) {
			//What ?
		}
		
		this.showed.Remove(menu);
		
		GMenu newCurrent = this.Current;
		if (newCurrent != null && newCurrent != oldCurrent) {
			newCurrent.foreground();
		}
	}

}
