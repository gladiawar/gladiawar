using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Controller for input customization
 * 
 * @prefab GOptionsMenu
 * @author Claude Ramseyer 
 */
public class GConfigInputs : MonoBehaviour {
	
	//Editor attributes
	public GameObject model;
	public UIGrid grid;
	
	//Attributes
	protected List<GInput> inputs;
	protected bool initialised = false;
	
	public void Start() {
		if (!this.initialised) {
			this.model.SetActive(false);
			
			this.inputs = new List<GInput>();
			
			//Add inputs
			
			
			//Reorganise
			grid.repositionNow = true;
			
			this.initialised = true;
		}
	}
	
}
