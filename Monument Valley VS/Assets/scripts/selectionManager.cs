using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectionManager : MonoBehaviour
{
    public Camera cam;
    public int destination;
    public string selectableTag = "Selectable";

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform;
                if (selection.CompareTag(selectableTag))
                {
                    var selectionRenderer = selection.gameObject;
                    if (selectionRenderer != null)
                    {
                        destination = selectionRenderer.GetComponent<SelectableInt>().waypointInt;
                    }
                }
            }
        }
    }
}
