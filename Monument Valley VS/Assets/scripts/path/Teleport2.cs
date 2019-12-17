using System.Collections;
using UnityEngine;

public class Teleport2 : MonoBehaviour
{
    public GameObject teleport1;
    public GameObject cubeDelete;
    public GameObject player;
    public bool uit = false;

    void Update()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) < 0.1f)
        {
            uit = true;
            StartCoroutine("TeleportUpdate");
        }
        else if (player.GetComponent<Player>().currentWaypoint == 15 && player.GetComponent<Player>().walkIndex == 15 && player.GetComponent<Player>().destinationInt == 15 && uit == false)
        {
            teleport1.SetActive(false);
        }
        else if (player.GetComponent<Player>().currentWaypoint != 15 && player.GetComponent<Player>().walkIndex != 15 && player.GetComponent<Player>().destinationInt != 15 && uit == false)
        {
            StartCoroutine("updates");
        }
    }

    IEnumerator TeleportUpdate()
    {
        teleport1.SetActive(false);
        cubeDelete.SetActive(true);
        player.GetComponent<Player>().walkIndex--;
        player.GetComponent<Player>().GetNextWaypoint();
        player.transform.position = teleport1.transform.position;
        yield return new WaitForSeconds(0.5f);
        if (player.GetComponent<Player>().destinationInt != 15)
        {
            teleport1.SetActive(true);
        }
        uit = false;
    }

    IEnumerator updates()
    {
        if (player.GetComponent<Player>().omhoog == true)
        {
            teleport1.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            teleport1.SetActive(true);
        }
    }
}
