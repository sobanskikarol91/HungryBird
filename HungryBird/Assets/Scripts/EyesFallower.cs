using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesFallower : MonoBehaviour
{
    public Transform reference;
    Transform player;
    float maxDistanceEyeMove = 0.08f;
    
    public void SetFallowers(E_FallowerType _fallowerType)
    {
        if (_fallowerType == E_FallowerType.CAMERA)
            SetSightToCamera();
        else
            SetSightToPlayer();
    }

    void SetSightToCamera()
    {
        StopAllCoroutines();
        StartCoroutine(FallowCamera());
    }

    void SetSightToPlayer()
    {
        StopAllCoroutines();
        player = GameManager.instance.Player;
        StartCoroutine(FallowPlayer());
    }


    IEnumerator FallowCamera()
    {
        while (true)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            Follow(mousePosition);
            yield return null;
        }
    }

    IEnumerator FallowPlayer()
    {
        while (true)
        {
            Follow(player.position);
            yield return null;
        }
    }

    void Follow(Vector3 Targetposition)
    {
        Vector3 dir = (Targetposition - reference.position);
        dir = Vector3.ClampMagnitude(dir, maxDistanceEyeMove);
        transform.position = reference.position + dir;
    }

    private void OnDestroy()
    {
        GameManager.instance.RemovePlayerFallower(this);
    }

    private void OnEnable()
    {
        GameManager.instance.AddPlayerFollowers(this);
    }

    private void OnDisable()
    {
        GameManager.instance.RemovePlayerFallower(this);
    }
}