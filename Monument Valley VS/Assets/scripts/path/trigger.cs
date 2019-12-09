using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    public bool triggers = false;

    void OnTriggerEnter(Collider col)
    {
        triggers = true;
    }
    void OnTriggerExit(Collider col)
    {
        triggers = false;
    }
}
