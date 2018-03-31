using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : InteractiveObject
{
    public int value;
    public EPICKUP type;
    Animator _animator;
    FloatingTextController ftc;

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponentInChildren<Animator>();
    }
    
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.tag.Equals("Player"))
        {
            ftc = GetComponent<FloatingTextController>();
            ftc.CreateScoreText(value, type);

            string valueStr = value.ToString();

            if (type == EPICKUP.cash)
                GameManager.instance.Score += value;
            else if (type == EPICKUP.Consumable)
                collision.GetComponent<PlayerHealth>().AddHealth(value);

            else if (type == EPICKUP.Ammo)
                collision.GetComponent<PlayerAttack>().AddAmmo(value);
            else if (type == EPICKUP.ProtectedShield)
                collision.GetComponent<ProtectedShield>().ActiveShield(value);
            else if (type == EPICKUP.DamageShield)
                collision.GetComponent<DamageShield>().ActiveShield(value);

            PlayerPickedUp();
        }
    }

    void PlayerPickedUp()
    {
        isAlive = false;
        collider.enabled = false;
        _animator.SetBool("PickedUp", true);
        audioSource.Play();

        float animationLength = 1f;
        float maxlength = Mathf.Max(animationLength, audioSource.clip.length);
        Invoke("InformThatObjectDeath", maxlength);
    }

    protected  override void OnEnable()
    {
        base.OnEnable();
        _animator.SetBool("PickedUp", false);
    }
}
