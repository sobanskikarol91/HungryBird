using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    ObjectMover _objectMover;
    float lineLength=8f;
    public AudioSource _childAudio;

    private void Awake()
    {
        _objectMover = GetComponent<ObjectMover>();
    }


    IEnumerator Detector()
    {
        while(true)
        {
            if (Physics2D.Linecast(transform.position, new Vector2(transform.position.x, -lineLength), 1 << LayerMask.NameToLayer("Player")))
            {
                _childAudio.Play();
                _objectMover.MoveDown();
                break;
            }
            yield return null;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(Detector());
    }
}
