using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowControllButtons : MonoBehaviour
{
    public GameObject controllPanel;

    float displayTime = 2.5f;
    float timeFromStartToDisplayPanel = 0.7f;
    

    public void ControllTip()
    {
        Invoke("ShowPanel", timeFromStartToDisplayPanel);
        Invoke("HidePanel", displayTime+timeFromStartToDisplayPanel);
    }

    void ShowPanel()
    {
        controllPanel.GetComponent<Animator>().SetBool("Show", true);
    }

    void HidePanel()
    {
        controllPanel.GetComponent<Animator>().SetBool("Show", false);
    }
}
