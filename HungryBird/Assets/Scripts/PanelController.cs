using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    //Menu Animator has disablePanel after finish animation
    public void DisablePanel()
    {
        gameObject.SetActive(false);
    }
}
