using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InputFieldListener : MonoBehaviour
{
    public Text _IFText;
    Button _button;

    public Image icone;
    public Sprite enabledIcone;
    public Sprite disableIcone;
    private void Awake()
    {
        _button = GetComponent<Button>();
        StartCoroutine(CheckInputFieldLength());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator CheckInputFieldLength()
    {
        bool stateChanged=false;

        while (true)
        {
            if (_button.interactable != _IFText.text.Length > 0)
                stateChanged = true;

            if (_IFText.text.Length > 0 && stateChanged)
            {
                icone.sprite = enabledIcone;
                stateChanged = false;
            }
            else if(_IFText.text.Length == 0 && stateChanged)
            {
                icone.sprite = disableIcone;
                stateChanged = false;
            }

            _button.interactable = _IFText.text.Length > 0;
            yield return null;
        }
    }
}
