using UnityEngine;
using System.Collections;

/**
 * Controller for GInput
 * 
 * @prefab GInput
 * @author Claude Ramseyer
 */
public class GInput : MonoBehaviour {
	
	//Constant
	public const float LABEL_LENGTH = 50f;
	public const float INPUT_LENGTH = 100f;
	public const float MARGIN = 10f;
	public const float BORDER = 7f;
	
	
	//Editor attributes
	public UILabel label;
	public UIInput input;
	
	//Properties
	public string LabelText {
		get {
			return this.label.text;
		}
		set {
			this.label.text = value;
		}
	}
	public string InputText {
		get {
			return this.input.text;
		}
		set {
			this.input.text = value;
		}
	}
	public string InputDefault {
		get {
			return this.input.defaultText;
		}
		set {
			this.input.defaultText = value;
		}
	}
	public UIInput.OnSubmit OnSubmit {
		get {
			return this.input.onSubmit;
		}
		set {
			this.input.onSubmit = value;
		}
	}
	
	public UIInput.Validator Validator {
		get {
			return this.input.validator;
		}
		set {
			this.input.validator = value;
		}
	}
	
	//Functions
	public void setInputLength(float length = GInput.INPUT_LENGTH) {
		//Set line width with taking BORDER length
		var label = this.input.label;
		var corrected = length - (GInput.BORDER * 2);
		label.lineWidth = (int) corrected;
		
		//Set background scale
		var background = this.input.GetComponentInChildren<UISprite>();
		var scale = background.transform.localScale;
		background.transform.localScale = new Vector3(length, scale.y, scale.z);
	}
	
	public void setLabelLength(float length = GInput.LABEL_LENGTH) {
		//Set label length
		var lpos = this.label.transform.localPosition;
		this.label.transform.localPosition = new Vector3(length, lpos.y, lpos.z);
		
		//Move input
		var ipos = this.input.transform.localPosition;
		this.input.transform.localPosition = new Vector3(length + GInput.MARGIN, ipos.y, ipos.z);
	}
	
}
