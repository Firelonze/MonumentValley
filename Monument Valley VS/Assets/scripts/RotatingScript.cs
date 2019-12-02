using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RotatingScript : MonoBehaviour 
{
	private Vector3 myVector = new Vector3(0.0f, 1.0f, 0.0f);
	private float angle = 3.2f;

	private void OnMouseDrag()
	{
		float x = Input.GetAxis("Mouse X");
		transform.RotateAround(transform.position, myVector * Time.deltaTime * -x, angle);
	}

	private void OnMouseUp()
	{
		float snapRotationAngle = (Mathf.RoundToInt(transform.rotation.eulerAngles.y / 90)) * 90;
		StartCoroutine(RotateToSnapPoint(snapRotationAngle));

	}

	private IEnumerator RotateToSnapPoint(float snapRotation)
	{
		float dist = 0;
		while (dist < 1)
		{
			dist += Time.deltaTime * 2;
			float currentRot = Mathf.Lerp(transform.rotation.eulerAngles.y,snapRotation, dist);
			transform.rotation = Quaternion.Euler(0, currentRot, 0);
			yield return null;
			
		}
    }

}