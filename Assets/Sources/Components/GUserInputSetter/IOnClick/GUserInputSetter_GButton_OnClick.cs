using UnityEngine;
using System.Collections;

/**
 * Basic class controller of GUserInputSetter
 * 
 * @prefab GUserInputSetter
 */
public class GUserInputSetter_GButton_OnClick : GButton_OnClick {
	
	public GUserInputSetter input;
	
	public override void OnClick(GButton caller) {
		this.input.OnClick();
	}	
	
}
