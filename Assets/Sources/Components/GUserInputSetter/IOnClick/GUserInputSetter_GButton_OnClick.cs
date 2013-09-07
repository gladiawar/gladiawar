using UnityEngine;
using System.Collections;

public class GUserInputSetter_GButton_OnClick : GButton_OnClick {
	
	public GUserInputSetter input;
	
	public override void OnClick(GButton caller) {
		this.input.OnClick();
	}	
	
}
