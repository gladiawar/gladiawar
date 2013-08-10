using UnityEngine;
using System.Collections;

public class GButton_Button : GladiawarBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnClick() {
		this.transform.parent.GetComponent<GButton>().OnClick();
	}
	
}
