using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RotatingScript : MonoBehaviour //LegClamp 
{
	Vector3 myVector = new Vector3(0.0f, 1.0f, 0.0f);
	private float angle = 3.2f;
//	private LegClamp leg1;//
//	private LegClamp leg2	;
	private void Awake()
	{
//		leg1 = GameObject.Find("Leg1").GetComponent<LegClamp>();
//		leg2 = GameObject.Find("Leg2").GetComponent<LegClamp>();
	
	}
	private void OnMouseDrag()
	{
		float x = Input.GetAxis("Mouse X");
		transform.RotateAround(transform.position, myVector * Time.deltaTime * -x, angle);
	}

	private void OnMouseUp()
	{
		//	leg1.canSnap = true;
		//	leg2.canSnap = true;
		float snapRotationAngle = (Mathf.RoundToInt(transform.rotation.eulerAngles.y / 90)) * 90;
		StartCoroutine(RotateToSnapPoint(snapRotationAngle));

	}

	private IEnumerator RotateToSnapPoint(float snapRotation)
	{
		/*
		 * current rotation niet in de buurt zit van snapRotation
		 * rotate richting snaprotation
		while ???
			{

			yield return null;
			transform.rotation = Quaternion.Euler(0,snapRotation,0);
		}
		*/
		yield return null;
		transform.rotation = Quaternion.Euler(0, snapRotation, 0);
	}

}