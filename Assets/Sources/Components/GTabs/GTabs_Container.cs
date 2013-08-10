using UnityEngine;
using System.Collections;

public class GTabs_Container : GladiawarBehaviour {
	
	protected GTabs_Content content;
	
	public void OnActive(GTabs_Content content) {
		this.content = content;
		this.show();
	}
	
	protected void show() {
		Transform contentT = this.content.transform;
		Transform thisT = this.transform;
		
		contentT.localPosition = new Vector3(0f, 0f, 0f);
		contentT.localRotation = thisT.localRotation;
		contentT.localScale = thisT.localScale;
		contentT.gameObject.SetActive(true);
	}
	
}
