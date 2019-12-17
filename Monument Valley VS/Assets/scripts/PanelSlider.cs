using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSlider : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject exit;

    public void OpenMenu()
    {
        menu.SetActive(false);
        anim.Play("Panel_down1");
    }
    public void CloseMenu()
    {
        anim.Play("PanelUp");
        menu.SetActive(true);
    }
}
