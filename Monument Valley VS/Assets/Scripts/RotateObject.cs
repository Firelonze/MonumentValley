using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RotateObject : MonoBehaviour 
{
	private Vector3 myVector = new Vector3(0.0f, 1.0f, 0.0f);
	private float angle = Constants.tags.angleTag;
	private void OnMouseDrag()
	{
		float x = Input.GetAxis(Constants.inputs.mouseTag);
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
		while (dist < Constants.tags.timeTag)
		{
			dist += Time.deltaTime;
			float currentRot = Mathf.Lerp(transform.rotation.eulerAngles.y,snapRotation, dist);
			Debug.Log(transform.rotation.eulerAngles + "Euler b4");
			transform.rotation = Quaternion.Euler(0, currentRot, 0);
			Debug.Log(transform.rotation.eulerAngles + "Euler after");
			yield return null;
			
		}
	
		Debug.Log("Coroutine ended");
	}

}