using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class AttackButton : MonoBehaviour, IPointerDownHandler
{
    RectTransform imgRT;
    [HideInInspector]
    public PlayerAttack _playerAttack;

    void Start()
    {
        if (Application.platform != RuntimePlatform.Android)
            gameObject.SetActive(false);

        imgRT = GetComponent<RectTransform>();        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imgRT,
            eventData.position,
            eventData.pressEventCamera, 
            out pos))   
            _playerAttack.DropEgg();
    }
}
