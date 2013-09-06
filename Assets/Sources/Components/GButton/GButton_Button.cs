using UnityEngine;
using System.Collections;

/**
 * Intern class
 * 
 * @prefab GButton
 * @author Claude Ramseyer
 */
public class GButton_Button : MonoBehaviour {
	
	//NGui event
	void OnClick() {
		this.transform.parent.GetComponent<GButton>().OnClick();
	}
	
}
