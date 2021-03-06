﻿using UnityEngine;
using System.Collections;

/**
 * Basic class controller of GUserInputSetter
 * 
 * @prefab GUserInputSetter
 */
public class GUserInputSetter : MonoBehaviour {
	
	public UILabel text;
	public UILabel keyText;
	public KeyCode key;
	
	public GUserInputSetterHandler Handler { get; set; }
	public string Text {
		get { return this.text.text; }
		set { this.text.text = value; }
	}
	public KeyCode Key {
		get { return this.key; }
		set { this.key = value; this.keyText.text = value.ToString(); }
	}
	
	public void OnClick() {
		this.Handler.OnClick(this);
	}
	
}
