using System.Collections;
using UnityEngine;

public class Teleport6 : MonoBehaviour
{
    public GameObject teleport5;
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
        else if (player.GetComponent<Player>().currentWaypoint == 16 && player.GetComponent<Player>().walkIndex == 16 && player.GetComponent<Player>().destinationInt == 16 && uit == false)
        {
            teleport5.SetActive(false);
        }
        else if (player.GetComponent<Player>().currentWaypoint != 16 && player.GetComponent<Player>().walkIndex != 16 && player.GetComponent<Player>().destinationInt != 16 && uit == false)
        {
            StartCoroutine("updates");
        }
    }

    IEnumerator TeleportUpdate()
    {
        teleport5.SetActive(false);
        cubeDelete.SetActive(true);
        player.GetComponent<Player>().walkIndex++;
        player.GetComponent<Player>().GetNextWaypoint();
        player.transform.position = teleport5.transform.position;
        yield return new WaitForSeconds(0.5f);
        if (player.GetComponent<Player>().destinationInt != 16)
        {
            teleport5.SetActive(true);
        }
        uit = false;
    }

    IEnumerator updates()
    {
        if (player.GetComponent<Player>().destinationInt > 16)
        {
            yield return new WaitForSeconds(0.5f);
            teleport5.SetActive(true);
        }
        if (player.GetComponent<Player>().destinationInt < 16)
        {
            teleport5.SetActive(true);
        }
    }
}