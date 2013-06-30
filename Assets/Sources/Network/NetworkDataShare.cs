/*
 * 
 * 
 * 
 */

using						UnityEngine;

public class 				NetworkDataShare
{
	public float			posX;
	public float			posY;
	public float			posZ;
	public float			rotX;
	public float			rotY;
	public float			rotZ;
	public float			rotW;
	public float			velX;
	public float			velZ;
	
	public					NetworkDataShare(Vector3 position, Quaternion rotation, Vector3 velocity)
	{
		posX = position.x;
		posY = position.y;
		posZ = position.z;
		rotX = rotation.x;
		rotY = rotation.y;
		rotZ = rotation.z;
		rotW = rotation.w;
		velX = velocity.x;
		velZ = velocity.z;
	}
}
