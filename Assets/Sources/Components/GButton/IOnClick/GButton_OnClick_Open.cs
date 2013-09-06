using UnityEngine;
using System.Collections;

/**
 * Basic implementation for show (SetActive(true)) GameObject
 * 
 * @prefab GButton
 * @author Claude Ramseyer
 */
public class GButton_OnClick_Open : GButton_OnClick {
	
	//Editor attributes
	public GameObject open;
	
	//Functions
	public override void OnClick(GButton caller) {
		this.open.SetActive(true);
	}
	
}
