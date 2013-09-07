using UnityEngine;
using System.Collections;

/**
 * Basic class controller of GMenu
 * 
 * @prefab GMenu
 * @author Claude Ramseyer
 */
public class GMenu : MonoBehaviour {
	
	//Delegate declaration
	public delegate void PreShowHandler(GMenu menu);
	public delegate void PostShowHandler(GMenu menu);
	public delegate void PreHideHandler(GMenu menu);
	public delegate void PostHideHandler(GMenu menu);
	public delegate void PostBackgroundHandler(GMenu menu);
	public delegate void PostForegroundHandler(GMenu menu);
	
	//Attributes
	public PreShowHandler preShow;
	public PostShowHandler postShow;
	public PreHideHandler preHide;
	public PostHideHandler postHide;
	public PostBackgroundHandler postBackground;
	public PostForegroundHandler postForeground;
	
	protected GameObject menu;
	protected GMenuManager manager;
	
	//Properties
	public bool Active {
		get { return this.menu.activeInHierarchy; }
	}
	
	//Functions
	public virtual void init(GMenuManager manager) {
		//Call on Start of manager
		this.manager = manager;
		this.menu = this.gameObject.GetComponentInChildren<GMenuContainer>().gameObject;
		this.menu.SetActive(false);
	}

	public void show() {
		if (!this.menu.activeInHierarchy) {
			if (this.preShow != null) {
				this.preShow(this);
			}
			
			this.menu.SetActive(true);
			
			if (this.postShow != null) {
				this.postShow(this);
			}
		}
		
		//Always call foreground callback after show
		if (this.postForeground != null) {
			this.postForeground(this);
		}
	}
	
	public void hide() {
		if (this.menu.activeInHierarchy) {
			if (this.preHide != null) {
				this.preHide(this);
			}
			
			this.menu.SetActive(false);
			
			if (this.postHide != null) {
				this.postHide(this);
			}
		}
	}
	
	public void background() {
		this.menu.SetActive(false);
		
		if (this.postBackground != null) {
			this.postBackground(this);
		}
	}
	
	public void foreground() {
		this.menu.SetActive(true);
		
		if (this.postForeground != null) {
			this.postForeground(this);
		}
	}
	
}
