using UnityEngine;
using System.Collections;

public class GTabs_Header : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		var parent = this.transform.parent.GetComponent<GTabs>();
		float v = 0;
		foreach (var item in parent.items) {
			item.transform.parent = parent.transform;
			item.tabs = parent;
			var local = item.transform.localPosition;
			if (parent.order == GTabs_Order.HORIZONTAL) {
				Vector3 pos = new Vector3(v, local.y, local.z);
				item.transform.localPosition = pos;
				v += local.x + parent.padding;
			}
			else if (parent.order == GTabs_Order.VERTICAL) {
				Vector3 pos = new Vector3(local.x, v, local.z);
				item.transform.localPosition = pos;
				v += local.y + parent.padding;
			}
		}
	}
	
}
