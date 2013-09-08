using UnityEngine;
using System.Collections;

public static class Keyboard {

	public static KeyCode Escape = KeyCode.Escape;
	
	public static KeyCode Action_Forward = KeyCode.W;
	public static KeyCode Action_Back = KeyCode.S;
	public static KeyCode Action_Left = KeyCode.A;
	public static KeyCode Action_Right = KeyCode.D;
	
	public static KeyCode Action_Run = KeyCode.LeftShift;
	public static KeyCode Action_Jump = KeyCode.Space;
	
	public static KeyCode Action_Strick = KeyCode.Mouse0;
	public static KeyCode Action_Block = KeyCode.Mouse1;
	
	public static KeyCode Action_SwitchWeapon_1 = KeyCode.Alpha1;
	public static KeyCode Action_SwitchWeapon_2 = KeyCode.Alpha2;
	public static KeyCode Action_SwitchWeapon_3 = KeyCode.Alpha3;
	
	public static KeyCode Action_Action = KeyCode.F;
	
	public static bool changeKey(KeyCode old, KeyCode key)
	{
		
		if (old == Keyboard.Action_Forward) {
			Keyboard.Action_Forward = key;	
		}
		else if (old == Keyboard.Action_Back) {
			Keyboard.Action_Back = key;
		}
		else if (old == Keyboard.Action_Left) {
			Keyboard.Action_Left = key;
		}
		else if (old == Keyboard.Action_Right) {
			Keyboard.Action_Right = key;
		}
		else if (old == Keyboard.Action_Run) {
			Keyboard.Action_Run = key;
		}
		else if (old == Keyboard.Action_Jump) {
			Keyboard.Action_Jump = key;
		}
		else if (old == Keyboard.Action_Action) {
			Keyboard.Action_Action = key;
		}
		else {
			return false;
		}
		
		return true;
	}
	
	public static void loadConfiguration() {
		
		//TODO	
	}
	
	public static void saveConfiguration() {
		//TODO
	}
	
}
