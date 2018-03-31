using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerController : MonoBehaviour
{
    Animator _anim;
    AudioSource _audioS;

    void Start()
    {
        _audioS = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
    }

    public void Warning()
    {
        _audioS.Play();
        _anim.SetTrigger("Warning");
    }
}
