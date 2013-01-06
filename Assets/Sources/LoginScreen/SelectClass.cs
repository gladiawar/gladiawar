/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public class 				SelectClass : MonoBehaviour
{
	public enum 			eClass
	{
		LIGHT,
		MEDIUM,
		HEAVY
	}
	
	public Transform		_otherButton, _otherButton2, _buttonCreate;
	public eClass			_class;
	
	void					Start()
	{
		if (_class == eClass.LIGHT)
			transform.GetComponent<UIButton>().isEnabled = false;
	}
	
	void					OnClick()
	{
		_otherButton.GetComponent<UIButton>().isEnabled = true;
		_otherButton2.GetComponent<UIButton>().isEnabled = true;
		transform.GetComponent<UIButton>().isEnabled = false;
		_buttonCreate.GetComponent<CreateCharacter>().ClassChoosen = _class;
	}
}
