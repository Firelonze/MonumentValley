using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
    public GameObject player;
    public bool button1Triggered = false;
    public bool button2Triggered = false;
    [SerializeField] private Animator anim;

    void Update()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) < 0.1f)
        {
            StartCoroutine("animatie");
        }
    }

    IEnumerator animatie()
    {
        if (name == "Button 1" && !button1Triggered)
        {
            yield return new WaitForSeconds(0.1f);
            button1Triggered = true;
            anim.SetBool("Rising", true);
        }
        if (name == "Button 2" && !button2Triggered)
        {
            yield return new WaitForSeconds(0.1f);
            button2Triggered = true;
            // zet hier de animaties die moeten gebeuren bij button 2
        }
    }
}
