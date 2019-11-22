using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegClamp : MonoBehaviour
{

//	public GameObject clamp;
	[SerializeField] public Transform target;
	private Collider[] collidersInRange;

	private BoxCollider col;

	// Start is called before the first frame update
	void Start()
    {
	col = gameObject.GetComponent<BoxCollider>();
    }

    

	private void OnMouseUp()
	{
		for (int i = 0; i < collidersInRange.Length; i++)
		{
			col.transform.rotation = target.transform.rotation;
			Debug.Log("I AM CLAMPING");
		}
		Debug.Log("I AM LETTING GO");	
	}
}
