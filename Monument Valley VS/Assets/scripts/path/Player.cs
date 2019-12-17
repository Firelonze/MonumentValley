using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public GameObject currentWaypointObject;
    public float step = 2f;
    public Vector3 currentWaypointVector;

    public int walkIndex = 0;
    public int destinationInt;

    public GameObject pathRefresher;
    public GameObject selectionManager;

    public bool omhoog;

    public int currentWaypoint;

    public bool moving = false;

    void Update()
    {
        if (moving == false)
        {
            anim.SetBool("Moving", false);
        }
        if (selectionManager.GetComponent<selectionManager>().destination != 0 && moving == false)
        {
            moving = true;
            destinationInt = selectionManager.GetComponent<selectionManager>().destination;
            Debug.Log(destinationInt);
            GetNextWaypoint();
        }

        if (omhoog == true)
        {
            if (walkIndex >= destinationInt)
            {
                selectionManager.GetComponent<selectionManager>().destination = 0;

                if (Vector3.Distance(currentWaypointVector, this.transform.position) < 0.01)
                {
                    moving = false;
                }
            }
        }
        if (omhoog == false)
        {
            if (walkIndex <= destinationInt)
            {
                selectionManager.GetComponent<selectionManager>().destination = 0;

                if (Vector3.Distance(currentWaypointVector, this.transform.position) < 0.01)
                {
                    moving = false;
                }
            }
        }

        if (moving == true)
        {
            anim.SetBool("Moving", true);
            if (Vector3.Distance(currentWaypointVector, this.transform.position) < 0.01)
            {
                if (walkIndex == 10 && destinationInt >= 13 && pathRefresher.GetComponent<PlayerPath>().array6Aan == true)
                {
                    walkIndex = 13;
                }
                else if (walkIndex == 11 && destinationInt >= 13 && pathRefresher.GetComponent<PlayerPath>().array6Aan == true || walkIndex == 12 && destinationInt >= 13 && pathRefresher.GetComponent<PlayerPath>().array6Aan == true)
                {
                    walkIndex--;
                }
                else if (omhoog == true)
                {
                    walkIndex++;
                }
                if (walkIndex == 13 && destinationInt <= 13 && pathRefresher.GetComponent<PlayerPath>().array6Aan == true)
                {
                    walkIndex = 10;
                }
                else if (omhoog == false)
                {
                    walkIndex--;
                }

                GetNextWaypoint();
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypointVector, step);
            transform.LookAt(currentWaypointVector);
            if (currentWaypoint == 13 && pathRefresher.GetComponent<PlayerPath>().array3Aan == true || currentWaypoint == 14 && pathRefresher.GetComponent<PlayerPath>().array3Aan == true)
            {
                transform.eulerAngles = new Vector3(0, 0, this.transform.eulerAngles.z);
            }
            else 
            {
                transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
                
            }
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
        currentWaypointVector = new Vector3(currentWaypointObject.transform.position.x, currentWaypointObject.transform.position.y + 0.249f, currentWaypointObject.transform.position.z);
    }
}