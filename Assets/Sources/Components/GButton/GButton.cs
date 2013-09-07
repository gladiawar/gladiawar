using UnityEngine;
using System.Collections;

/**
 * Main class for prefab GButton
 * 
 * @prefab GButton
 * @author Claude Ramseyer
 */
public class GButton : MonoBehaviour {
	
	//Attributes
	protected GButton_OnClick[] callbacks;
	protected UILabel label;
	
	//Properties
	public string Text {
		get { return this.label.text; }
		set { this.label.text = value; }
	}
	
	//Game logic
	void Start () {
		this.callbacks = this.transform.GetComponents<GButton_OnClick>();
		this.label = this.GetComponentInChildren<GButton_Button>().GetComponentInChildren<UILabel>();
	}
	
	//Functions
	public void OnClick() {
		foreach(var callback in this.callbacks) {
			callback.OnClick(this);	
		}
	}
	
}
