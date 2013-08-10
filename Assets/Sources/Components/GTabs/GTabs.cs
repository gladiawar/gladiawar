using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * 
 * @author Claude Ramseyer
 */
public class GTabs : GladiawarBehaviour {
	
	public List<GTabButton> items;
	public GTabButton current;
	public GTabs_Container container;
	
	public GTabs_Order order;
	public float padding = 0;
	
	public void Start() {
		if (this.items.Count > 0) {
			float x = 0;
			foreach (var item in this.items) {
				this.addTabInit(item);
				
				Transform transform = item.transform;
				transform.localPosition = new Vector3(x, 0f, 0f);
				
				x += this.padding + transform.localScale.x;
			}
			
			if (this.current == null) {
				this.current = this.items[0];
			}
			this.OnActive(this.current);
		}
	}
	
	public void OnActive(GTabButton caller) {
		this.container.OnActive(caller.content);
	}
	
	public void addTab(GTabButton button) {
		this.items.Add(button);
		this.addTabInit(button);
	}
	
	protected void addTabInit(GTabButton button) {
		button.tabs = this;
	}
	
}
