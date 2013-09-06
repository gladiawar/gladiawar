using UnityEngine;
using System.Collections;

/**
 * Basic implementation for hide (SetActive(false)) GameObject
 * 
 * @prefab GButton
 * @author Claude Ramseyer
 */
public class GButton_OnClick_Close : GButton_OnClick {
	
	//Editor attributes
	public GameObject close;
	
	//Functions
	public override void OnClick(GButton caller) {
		this.close.SetActive(false);
	}
	
}
