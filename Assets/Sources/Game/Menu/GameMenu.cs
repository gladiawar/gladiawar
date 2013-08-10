using UnityEngine;
using System.Collections;

public class GameMenu : MonoBehaviour {
	
	public GameObject container;
	
	// Use this for initialization
	void Start () {
		this.container.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(Keyboard.Escape)) {
			bool active = this.container.activeInHierarchy;
			this.container.SetActive(!active);
		}
	}
	
}
