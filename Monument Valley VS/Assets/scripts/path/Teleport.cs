using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour
{
    public GameObject teleport1;
    public GameObject teleport2;
    public GameObject teleport3;
    public GameObject teleport4;
    public GameObject cubeDelete;
    public GameObject player;
    
    
    void Update()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) < 0.1f)
        {
            StartCoroutine("TeleportUpdate");
        }
        if (player.GetComponent<Player>().walkIndex == 15)
        {
            teleport1.SetActive(false);
        }
        else if (player.GetComponent<Player>().destinationInt != 15)
        {
            teleport1.SetActive(true);
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
        else if (name == "teleport 2")
        {
            teleport1.SetActive(false);
            cubeDelete.SetActive(true);
            player.GetComponent<Player>().walkIndex--;
            player.GetComponent<Player>().GetNextWaypoint();
            player.transform.position = teleport1.transform.position;
            yield return new WaitForSeconds(1f);
            teleport1.SetActive(true);
        }
        else if (name == "teleport 3")
        {
            cubeDelete.SetActive(false);
            player.GetComponent<Player>().walkIndex += 2;
            player.GetComponent<Player>().GetNextWaypoint();
            player.transform.position = new Vector3(teleport4.transform.position.x + 0.02f, teleport4.transform.position.y, teleport4.transform.position.z);
        }
        else if (name == "teleport 4")
        {
            cubeDelete.SetActive(true);
            player.GetComponent<Player>().walkIndex--;
            player.GetComponent<Player>().GetNextWaypoint();
            player.transform.position = new Vector3(teleport3.transform.position.x - 0.02f, teleport3.transform.position.y, teleport3.transform.position.z);
        }
    }
}
