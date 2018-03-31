using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image bgImg;
    private Image joystickImg;
    private Vector2 inputVector;

    private void Start()
    {
        bgImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
             bgImg.rectTransform,
             ped.position,
             ped.pressEventCamera,
             out pos))
        {

            pos.x = pos.x /bgImg.rectTransform.sizeDelta.x;
            pos.y = pos.y /bgImg.rectTransform.sizeDelta.y;


            inputVector = new Vector2(pos.x * 2, pos.y * 2);
            inputVector = (inputVector.magnitude > 1f) ? inputVector.normalized : inputVector;

            // Move Joystick IMG
            joystickImg.rectTransform.anchoredPosition =
                new Vector3(inputVector.x * bgImg.rectTransform.sizeDelta.x / 5f,
                inputVector.y * bgImg.rectTransform.sizeDelta.y / 5f);
        }
    }

    public void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float Horizontal()
    {
        if (inputVector.x != 0)
            return inputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (inputVector.x != 0)
            return inputVector.y;
        else
            return Input.GetAxis("Vertical");
    }
}
