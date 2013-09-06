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
	
	//Game logic
	void Start () {
		this.callbacks = this.transform.GetComponents<GButton_OnClick>();
	}
	
	//Functions
	public void OnClick() {
		foreach(var callback in this.callbacks) {
			callback.OnClick(this);	
		}
	}
	
}
