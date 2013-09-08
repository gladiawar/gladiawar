using UnityEngine;
using System.Collections;

public abstract class GMenuListener : MonoBehaviour {
	
	public virtual void preShow() {}
	public virtual void postShow() {}
	public virtual void preHide() {}
	public virtual void postHide() {}
	public virtual void postBackground() {}
	public virtual void postForeground() {}
	
}
