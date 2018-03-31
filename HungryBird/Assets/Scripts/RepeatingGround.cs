using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingGround : MonoBehaviour
{
    public SpriteRenderer ground_SR;
    float groundLengthX;

    void Start()
    {
        groundLengthX = ground_SR.bounds.size.x * transform.childCount;
    }

    void Update()
    {
        if (transform.localPosition.x < -groundLengthX)
        {
            Vector2 groundOffset = new Vector2(2 * groundLengthX, 0);
            transform.Translate(groundOffset);
        }
    }
}
