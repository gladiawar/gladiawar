using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	
	public static bool changeKey(KeyCode old, KeyCode key) {
		Debug.Log("Key Changed " + old.ToString() + " => " + key.ToString());
		
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
		
		Keyboard.saveConfiguration();
		return true;
	}
	
	public static void loadConfiguration() {
		if (ES2.Exists("keyboard")) {
			Dictionary<string, int> config = ES2.LoadDictionary<string, int>("keyboard");
			
			Keyboard.Escape = config.ContainsKey("escape") ? (KeyCode) config["escape"] : KeyCode.Escape;
			Keyboard.Escape = config.ContainsKey("forward") ? (KeyCode) config["forward"] : KeyCode.W;
			Keyboard.Escape = config.ContainsKey("back") ? (KeyCode) config["back"] : KeyCode.S;
			Keyboard.Escape = config.ContainsKey("left") ? (KeyCode) config["left"] : KeyCode.A;
			Keyboard.Escape = config.ContainsKey("right") ? (KeyCode) config["right"] : KeyCode.D;
			Keyboard.Escape = config.ContainsKey("run") ? (KeyCode) config["run"] : KeyCode.LeftShift;
			Keyboard.Escape = config.ContainsKey("jump") ? (KeyCode) config["jump"] : KeyCode.Space;
			Keyboard.Escape = config.ContainsKey("strick") ? (KeyCode) config["strick"] : KeyCode.Mouse0;
			Keyboard.Escape = config.ContainsKey("block") ? (KeyCode) config["block"] : KeyCode.Mouse1;
			Keyboard.Escape = config.ContainsKey("switchw1") ? (KeyCode) config["switchw1"] : KeyCode.Alpha1;
			Keyboard.Escape = config.ContainsKey("switchw2") ? (KeyCode) config["switchw2"] : KeyCode.Alpha2;
			Keyboard.Escape = config.ContainsKey("switchw3") ? (KeyCode) config["switchw3"] : KeyCode.Alpha3;
			Keyboard.Escape = config.ContainsKey("action") ? (KeyCode) config["action"] : KeyCode.F;
		}
	}
	
	public static void saveConfiguration() {
		Dictionary<string, int> config = new Dictionary<string, int>();
		
		config["escape"] = Keyboard.Escape;
		config["forward"] = Keyboard.Action_Forward;
		config["back"] = Keyboard.Action_Back;
		config["left"] = Keyboard.Action_Left;
		config["right"] = Keyboard.Action_Right;
		config["run"] = Keyboard.Action_Run;
		config["jump"] = Keyboard.Action_Jump;
		config["strick"] = Keyboard.Action_Strick;
		config["block"] = Keyboard.Action_Block;
		config["switchw1"] = Keyboard.Action_SwitchWeapon_1;
		config["switchw2"] = Keyboard.Action_SwitchWeapon_2;
		config["switchw3"] = Keyboard.Action_SwitchWeapon_3;
		config["action"] = Keyboard.Action_Action;
		
		ES2.Save(config, "keyboard");
	}
	
	private static KeyCode load(string name, KeyCode key) {
		if (ES2.Exists(name)) {
			return (KeyCode) ES2.Load<int>(name);
		}
		return key;
	}
	
}
