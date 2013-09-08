using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Basic manager of a set of menu
 * 
 * @prefab GMenu
 * @author Claude Ramseyer
 */
public class GMenuManager : MonoBehaviour {
	
	//Attributes
	protected GMenu[] menus;
	protected List<GMenu> showed;
	
	//Properties
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
	public bool Active {
		get {
			return this.showed.Count != 0;
		}
	}
	
	//Game logic
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
	
	//Delegate implementation
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
