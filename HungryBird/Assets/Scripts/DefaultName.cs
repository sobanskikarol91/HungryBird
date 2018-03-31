using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultName : MonoBehaviour
{
    public Text enterTxt;
    public InputField _if;

    private void Awake()
    {
        _if.text = PlayerPrefs.GetString("defaultName");
        _if.Select();
        //_if.ActivateInputField();
    }

    public void SaveName()
    {
        PlayerPrefs.SetString("defaultName", enterTxt.text);
    }
}
