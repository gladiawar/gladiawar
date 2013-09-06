using UnityEngine;
using System.Collections;

/**
 * Interface for callback OnClick button
 * 
 * @prefab GButton
 * @author Claude Ramseyer
 */
public abstract class GButton_OnClick : MonoBehaviour {
	
	//Abstract functions
	public abstract void OnClick(GButton caller);
	
}
