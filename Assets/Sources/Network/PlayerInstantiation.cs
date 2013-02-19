using UnityEngine;
using System.Collections;

public class PlayerInstantiation : MonoBehaviour
{
	void OnNetworkInstantiate (NetworkMessageInfo info)
	{
		Debug.Log ("New object instantiated by " + info.sender);
		if (networkView.isMine) {
			name = "Local " + name;
			GetComponent<CharacterController> ().enabled = true;
			GetComponent<CharacterMotor> ().canControl = true;
			GetComponent<FPSInputController> ().enabled = true;
			GetComponent<SimpleAttack>().enabled = true;
			GetComponentInChildren<Camera> ().enabled = true;
			GetComponentInChildren<GUILayer> ().enabled = true;
			GetComponentInChildren<AudioListener> ().enabled = true;
					
			/* 
			 * Two mouse look by the default asset !(Wut wut ?)
			 * Une pour le corps, et l'autre pour la caméra
			 * Sinon ça fait des trucs étranges quand on essaie de bouger
			 */
			foreach (MouseLook ml in GetComponentsInChildren<MouseLook>()) {
				ml.enabled = true;	
			}
			
		}
	}
}
