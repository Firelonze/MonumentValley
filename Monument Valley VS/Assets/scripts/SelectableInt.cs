using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableInt : MonoBehaviour
{
    public int waypointInt;
    void Start()
    {
        switch (name)
        {
            case "1":
                waypointInt = 1;
                break;
            case "2":
                waypointInt = 2;
                break;
            case "3":
                waypointInt = 3;
                break;
            case "4":
                waypointInt = 4;
                break;
            case "5":
                waypointInt = 5;
                break;
            case "6":
                waypointInt = 6;
                break;
            case "7":
                waypointInt = 7;
                break;
            case "8":
                waypointInt = 8;
                break;
            case "9":
                waypointInt = 9;
                break;
        }
    }
}
