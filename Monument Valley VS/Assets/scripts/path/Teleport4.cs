using System.Collections;
using UnityEngine;

public class Teleport4 : MonoBehaviour
{
    public GameObject teleport3;
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
        else if (player.GetComponent<Player>().currentWaypoint == 26 && player.GetComponent<Player>().walkIndex == 26 && player.GetComponent<Player>().destinationInt == 26 && uit == false)
        {
            teleport3.SetActive(false);
        }
        else if (player.GetComponent<Player>().currentWaypoint != 26 && player.GetComponent<Player>().walkIndex != 26 && player.GetComponent<Player>().destinationInt != 26 && uit == false)
        {
            StartCoroutine("updates");
        }
    }

    IEnumerator TeleportUpdate()
    {
        teleport3.SetActive(false);
        cubeDelete.SetActive(true);
        player.GetComponent<Player>().walkIndex--;
        player.GetComponent<Player>().GetNextWaypoint();
        player.transform.position = teleport3.transform.position;
        yield return new WaitForSeconds(0.5f);
        if (player.GetComponent<Player>().destinationInt != 26)
        {
            teleport3.SetActive(true);
        }
        uit = false;
    }

    IEnumerator updates()
    {
        if (player.GetComponent<Player>().omhoog == true)
        {
            teleport3.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            teleport3.SetActive(true);
        }
    }
}