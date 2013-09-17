/*
 * 
 * 
 * 
 */

using						UnityEngine;
using						System.Collections;

public class 				UpdateMapNameLabel : MonoBehaviour
{
	string					currentValue;
	
	void					Update()
	{
		if (RunTimeData.MapName != currentValue)
		{
			transform.GetComponent<UILabel>().text = RunTimeData.MapName;
			currentValue = RunTimeData.MapName;
		}
	}
}
