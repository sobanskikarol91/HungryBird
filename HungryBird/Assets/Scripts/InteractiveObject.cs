using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(ObjectMover), typeof(AudioSource), typeof(DeathAnimationController))]
public class InteractiveObject : MonoBehaviour
{
    protected AudioSource audioSource;
    protected SpriteRenderer sr;
    protected Collider2D collider;

    [HideInInspector]
    public bool isAlive = false;              // protected to destroy object few times
    DeathAnimationController _deathController;
    SpawnManagerInformator _smInformator;


    protected virtual void Awake()
    {
        _smInformator = GetComponent<SpawnManagerInformator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<Collider2D>();
        _deathController = GetComponent<DeathAnimationController>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Death()
    {
        isAlive = false;
        collider.enabled = false;
        ShowSpriteRenderer(false);
        ShowDeathEffect();
        audioSource.Play();

        float animationLength = 0.55f;

        float maxlength = Mathf.Max(animationLength, audioSource.clip.length);
        Invoke("InformThatObjectDeath", maxlength);
    }

    void ShowSpriteRenderer(bool state)
    {
        sr.enabled = state;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAlive)  return;
    }

    public virtual void InstantDestroy()
    {
        Death();
    }

    protected void InformThatObjectDeath()
    {
        if (_smInformator == null) return;
        _smInformator.InformSpawnManager();
    }

    protected virtual void OnEnable()
    {
        isAlive = true;
        collider.enabled = true;
        ShowSpriteRenderer(true);
    }

    private void OnDisable()
    {
        isAlive = false;
    }

     public void ShowDeathEffect()
    {
        if(_deathController == null) Debug.Log(gameObject.name);
        _deathController.ShowDeathEffect();
    }
}
