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
	public GUserInputSetterHandler handler;
	public GameObject model;
	public UIGrid grid;
	
	//Attributes
	protected List<GUserInputSetter> inputs;
	protected bool initialised = false;
	
	public void Start() {
		this.model.SetActive(false);
		
		this.inputs = new List<GUserInputSetter>();
		
		//Add inputs
		this.addNewInput("Forward", Keyboard.Action_Forward);
		this.addNewInput("Back", Keyboard.Action_Back);
		this.addNewInput("Left", Keyboard.Action_Left);
		this.addNewInput("Right", Keyboard.Action_Right);
		this.addNewInput("Run", Keyboard.Action_Run);
		this.addNewInput("Jump", Keyboard.Action_Jump);
		this.addNewInput("Action", Keyboard.Action_Action);
		
		//Reorganise
		grid.repositionNow = true;
	}
	
	public GameObject addNewInput(string text, KeyCode key) {
		var go = (GameObject) Instantiate(this.model);
		var input = go.GetComponent<GUserInputSetter>();
		
		if (input != null) {
			input.Handler = this.handler;
			input.Text = text;
			input.Key = key;
			
			go.transform.parent = this.grid.transform;
			go.transform.localPosition = new Vector3(0, 0, 0);
			go.transform.localScale = new Vector3(1f, 1f, 1f);
			go.SetActive(true);
			
			//this.inputs.Add(input);
			return go;
		}
		
		return null;
	}
	
}
