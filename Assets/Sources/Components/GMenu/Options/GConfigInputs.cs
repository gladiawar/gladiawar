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
			for (int i = 0; i < 5; i++) {
				var go = (GameObject) Instantiate(this.model);
				var input = go.GetComponent<GInput>();
				go.transform.parent = this.grid.transform;
				go.transform.localScale = new Vector3(1f, 1f, 1f);
				go.transform.localPosition = new Vector3(0, 0, 0);
				go.SetActive(true);
				
				this.inputs.Add(input);
			}
			
			//Reorganise
			grid.repositionNow = true;
			
			this.initialised = true;
		}
	}
	
}
