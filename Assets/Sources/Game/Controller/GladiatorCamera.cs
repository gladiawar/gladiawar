/*
 *
 *
 *
 */

using						UnityEngine;
using						System.Collections;

public class 				GladiatorCamera : MonoBehaviour
{
	public float			_height;
	public float			_distance;
	public float			_headSet;
	
	Transform				_cam;
	
	void					Start()
	{
		_cam = Camera.main.transform;
	}
	
	void					LateUpdate()
	{
		float			currentYRotation = transform.eulerAngles.y;
		Quaternion		RotationY = Quaternion.Euler(0, currentYRotation, 0);
			
		_cam.position = transform.position;
		_cam.position += (RotationY * Vector3.back * _distance);
		_cam.position = new Vector3(_cam.position.x, transform.position.y + _height, _cam.position.z);
		_cam.LookAt(transform.position + new Vector3(0, _headSet, 0));
	}
}
