using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingScript : MonoBehaviour
{
	Vector3 myVector = new Vector3(0.0f, 1.0f, 0.0f);
	private float angle = 6;

	private void OnMouseDrag()
	{
		float x = Input.GetAxis("Mouse X");
		transform.RotateAround(transform.position, myVector*Time.deltaTime*-x,angle);
	}
}
