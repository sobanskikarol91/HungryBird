using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimationController : MonoBehaviour
{
    public float deathScaleEffect = 0.2f;
    public GameObject deathEffectPrefab;
    GameObject deathEffect;
    Animator _deathAnimator;


    void Awake()
    {
        deathEffect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        _deathAnimator = deathEffect.GetComponent<Animator>();
        deathEffect.transform.localScale *= deathScaleEffect;
        deathEffect.transform.SetParent(gameObject.transform);
    }

    public void ShowDeathEffect()
    {
        _deathAnimator.SetTrigger("DeathEffect");
    }
}
