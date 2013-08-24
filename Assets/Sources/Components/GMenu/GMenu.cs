using UnityEngine;
using System.Collections;

public class GMenu : GladiawarBehaviour {
	
	//Delegate
	public delegate void PreShow(GMenu menu);
	public delegate void PostShow(GMenu menu);
	public delegate void PreHide(GMenu menu);
	public delegate void PostHide(GMenu menu);
	public delegate void PostBackground(GMenu menu);
	public delegate void PostForeground(GMenu menu);
	
	public PreShow preShow;
	public PostShow postShow;
	public PreHide preHide;
	public PostHide postHide;
	public PostBackground postBackground;
	public PostForeground postForeground;
	
	protected GameObject menu;
	protected GMenuManager manager;
	
	public bool Active {
		get { return this.menu.activeInHierarchy; }
	}
	
	//Call on Start of manager
	public virtual void init(GMenuManager manager) {
		this.manager = manager;
		this.menu = this.gameObject.GetComponentInChildren<GMenuContainer>().gameObject;
		this.menu.SetActive(false);
	}

	public void show() {
		Debug.Log("SHOW : " + this.GetType().Name);
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
		Debug.Log("HIDE : " + this.GetType().Name);
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
		Debug.Log("BACK : " + this.GetType().Name);
		this.menu.SetActive(false);
		
		if (this.postBackground != null) {
			this.postBackground(this);
		}
	}
	
	public void foreground() {
		Debug.Log("HIDE : " + this.GetType().Name);
		this.menu.SetActive(true);
		
		if (this.postForeground != null) {
			this.postForeground(this);
		}
	}
	
}
