using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip highlighted;
    public AudioClip clicked;
    public AudioSource _audioSource;
    Button _button;

    private void Start()
    {
         _button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _audioSource.clip = highlighted;
        _audioSource.Play();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _audioSource.clip = clicked;
        _audioSource.Play();
    }
}
