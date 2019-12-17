using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour
{
    public GameObject teleport1;
    public GameObject teleport2;
    public GameObject teleport3;
    public GameObject teleport4;
    public GameObject teleport5;
    public GameObject teleport6;
    public GameObject cubeDelete;
    public GameObject player;
    
    
    
    void Update()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) < 0.1f && teleport2.GetComponent<Teleport2>().uit == false && teleport4.GetComponent<Teleport4>().uit == false && teleport6.GetComponent<Teleport6>().uit == false)
        {
            StartCoroutine("TeleportUpdate");
        }
    }

    IEnumerator TeleportUpdate()
    {
        if (name == "teleport 1")
        {
            teleport2.SetActive(false);
            cubeDelete.SetActive(false);
            player.GetComponent<Player>().walkIndex++;
            player.GetComponent<Player>().GetNextWaypoint();
            player.transform.position = teleport2.transform.position;
            yield return new WaitForSeconds(0.5f);
            teleport2.SetActive(true);
        }
        else if (name == "teleport 3")
        {
            teleport4.SetActive(false);
            cubeDelete.SetActive(false);
            player.GetComponent<Player>().walkIndex++;
            player.GetComponent<Player>().GetNextWaypoint();
            player.transform.position = teleport4.transform.position;
            yield return new WaitForSeconds(0.5f);
            teleport4.SetActive(true);
        }
        else if (name == "teleport 5")
        {
            teleport6.SetActive(false);
            cubeDelete.SetActive(false);
            player.GetComponent<Player>().walkIndex--;
            player.GetComponent<Player>().GetNextWaypoint();
            player.transform.position = teleport6.transform.position;
            yield return new WaitForSeconds(0.5f);
            teleport6.SetActive(true);
        }
    }
}
