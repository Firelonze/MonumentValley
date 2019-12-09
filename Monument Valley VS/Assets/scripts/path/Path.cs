﻿using UnityEngine;

public class Path : MonoBehaviour
{
    public bool array3Aan = false;
    public bool array6Aan = false;
    public bool array3AanUpdate = true;
    public bool array6AanUpdate = true;

    public GameObject player;

    public GameObject trigger1;
    public GameObject trigger2;
    public GameObject trigger3;

    public GameObject Array1;
    public GameObject Array2;
    public GameObject Array3;
    public GameObject Array4;
    public GameObject Array5;
    public GameObject Array6;
    public GameObject Array7;

    void Start()
    {
        InvokeRepeating("pathUpdate", 0f, 0.1f);
        InvokeRepeating("playerPosition", 0f, 0.1f);
    }

    void playerPosition()
    {
        if (player.transform.position.x < -13.5f && array3AanUpdate == true)
        {
            array3Aan = true;
        }
        if (player.transform.position.x > -13.5f && array3AanUpdate == true)
        {
            array3Aan = false;
        }
        if (player.transform.position.x > -6.5f && array6AanUpdate == true)
        {
            array6Aan = true;
        }
        if (player.transform.position.x < -6.5f && array6AanUpdate == true)
        {
            array6Aan = false;
        }
    }

    void pathUpdate()
    {
        if (trigger1.GetComponent<trigger>().triggers == false && player.GetComponent<Player>().walkIndex <= 2)
        {
            Array1.SetActive(true);
        }
        else
        {
            Array1.SetActive(false);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (trigger1.GetComponent<trigger>().triggers == true && trigger2.GetComponent<trigger>().triggers == true && array6Aan == false)
        {
            Array2.SetActive(true);
        }
        else
        {
            Array2.SetActive(false);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (player.GetComponent<Player>().walkIndex <= 10 && trigger2.GetComponent<trigger>().triggers == false && array3Aan == true)
        {
            Array3.SetActive(true);
        }
        else
        {
            Array3.SetActive(false);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (trigger2.GetComponent<trigger>().triggers == true && trigger1.GetComponent<trigger>().triggers == false && array3Aan == true)
        {
            array3AanUpdate = false;
            Array4.SetActive(true);
        }
        else
        {
            array3AanUpdate = true;
            Array4.SetActive(false);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (trigger1.GetComponent<trigger>().triggers == true && trigger3.GetComponent<trigger>().triggers == true && array3Aan == false)
        {
            Array5.SetActive(true);
        }
        else
        {
            Array5.SetActive(false);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (trigger3.GetComponent<trigger>().triggers == false && array6Aan == true)
        {
            Array6.SetActive(true);
        }
        else
        {
            Array6.SetActive(false);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (trigger3.GetComponent<trigger>().triggers == true && trigger1.GetComponent<trigger>().triggers == false && array6Aan == true)
        {
            array6AanUpdate = false;
            Array7.SetActive(true);
        }
        else
        {
            array6AanUpdate = true;
            Array7.SetActive(false);
        }
    }
}
