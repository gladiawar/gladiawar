using UnityEngine;
using System.Collections;

public class GUserInputSetterHandler : MonoBehaviour {
	
	public GuserInputSetterBox box;
	public GUserInputSetter input;
	
	public void OnClick(GUserInputSetter input) {
		if (this.input == null) {
			this.input = input;
			this.box.setHandlerAndShow(this);
		}
	}
	
	public void OnCaptureKey(KeyCode key) {
		if (this.input != null) {
			if (this.input.Key != key) {
				if (Keyboard.changeKey(this.input.Key, key)) {
					this.input.Key = key;
				}
			}
			this.box.unsetHandlerAndHide();
			this.input = null;
		}
	}
	
}
