using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegClamp : MonoBehaviour
{
	public Transform[] poles;
	public bool canSnap = false;
	private void Update()
	{
		if (canSnap)
		{
			Snap();
			canSnap = false;
		}

	}
	public void Snap()
	{
		Quaternion original = transform.rotation;
		Quaternion closestRotation = transform.rotation;
		float angle = float.PositiveInfinity;
		for (int i = 0; i < poles.Length; i++)
		{
			transform.LookAt(poles[i]);
			if (Quaternion.Angle(original, transform.rotation) < angle)
				closestRotation = transform.rotation;
		}
	}
}

