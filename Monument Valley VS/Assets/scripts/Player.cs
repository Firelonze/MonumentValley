using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject currentWaypointObject;
    public float step = 2f;
    public Vector3 currentWaypointTransform;

    public int walkIndex = 0;
    public int destinationInt;

    public bool omhoog;

    public int currentWaypoint;

    public bool moving = false;

    void Update()
    {
        if (GameObject.Find("Selection Manager").GetComponent<selectionManager>().destination != 0 && moving == false)
        {
            moving = true;
            destinationInt = GameObject.Find("Selection Manager").GetComponent<selectionManager>().destination;
            Debug.Log(destinationInt);
            GetNextWaypoint();
        }

        if (omhoog == true)
        {
            if (walkIndex >= destinationInt)
            {
                GameObject.Find("Selection Manager").GetComponent<selectionManager>().destination = 0;

                if (Vector3.Distance(currentWaypointTransform, this.transform.position) < 0.01)
                {
                    moving = false;
                }
            }
        }
        if (omhoog == false)
        {
            if (walkIndex <= destinationInt)
            {
                GameObject.Find("Selection Manager").GetComponent<selectionManager>().destination = 0;

                if (Vector3.Distance(currentWaypointTransform, this.transform.position) < 0.01)
                {
                    moving = false;
                }
            }
        }

        if (moving == true)
        {
            if (Vector3.Distance(currentWaypointTransform, this.transform.position) < 0.01)
            {
                if (omhoog == true)
                {
                    walkIndex++;
                }
                if (omhoog == false)
                {
                    walkIndex--;
                }
                GetNextWaypoint();
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypointTransform, step);
            transform.LookAt(currentWaypointTransform);
        }
    }

    public void GetNextWaypoint()
    {
        if (destinationInt >= walkIndex)
        {
            omhoog = true;
        }
        if (destinationInt <= walkIndex)
        {
            omhoog = false;
        }
        currentWaypoint = walkIndex;
        currentWaypointObject = GameObject.Find("" + currentWaypoint);
        currentWaypointTransform = new Vector3(currentWaypointObject.transform.position.x, 1.048f, currentWaypointObject.transform.position.z);
    }
}