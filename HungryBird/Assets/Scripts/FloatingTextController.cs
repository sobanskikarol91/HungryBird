using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour
{
    public GameObject popupPrefab;
    GameObject popup;
    Transform gameCanvas;
    FloatingText ft;



    public void CreateScoreText(int score)
    {
        TextSettings();
        ft.TextAppearance(score);
    }

    public void SetTextAppearance(int score)
    {
        ft.TextAppearance(score);
    }

    public void CreateScoreText(int score, EPICKUP type)
    {
        TextSettings();
        ft.TextAppearance(score, type);
    }

    void TextSettings()
    {
        popup = Instantiate(popupPrefab, transform.position, Quaternion.identity);
        ft = popup.GetComponent<FloatingText>();

        gameCanvas = GameObject.FindGameObjectWithTag("gameCanvas").transform;
        popup.transform.SetParent(gameCanvas, false);
        popup.transform.position = transform.position;
    }

}
