using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent(typeof(ObjectMover))]
public class PlayerController : InteractiveObject
{
    public float speed = 5f;
    public bool immortal;
    public AudioClip deathClip;
    public AudioMixer _mixer;
    float moveHorizontal;
    float moveVertical;
    Rigidbody2D rigidbody;
    PlayerHealth playerHealth;
    PlayerAttack _playerAttack;
    ProtectedShield protectedShield;
    DamageShield damageShield;
    ParticleSystem _particleSystem;
    LeftJoystick _joystick;
    float constrainX = 8f;
    float constrainY = 4.3f;


    protected override void Awake()
    {
        base.Awake();
        GameManager.instance.Player = gameObject.transform;
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _playerAttack = GetComponent<PlayerAttack>();
        protectedShield = GetComponent<ProtectedShield>();
        damageShield = GetComponent<DamageShield>();
        rigidbody = GetComponent<Rigidbody2D>();

        _joystick = GameObject.FindGameObjectWithTag("joystick").GetComponent<LeftJoystick>();
    }

    protected override void Start()
    {
        base.Start();
        playerHealth = GetComponent<PlayerHealth>();
    }

    IEnumerator PlayerControl()
    {
        while (isAlive)
        {
            moveHorizontal = _joystick.Horizontal();
            moveVertical = _joystick.Vertical(); ;

            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rigidbody.velocity = movement * speed;

            rigidbody.position = new Vector3(
                Mathf.Clamp(rigidbody.position.x, -constrainX, constrainX),
                Mathf.Clamp(rigidbody.position.y, -constrainY, constrainY),
                0);

            yield return null;
        }
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.tag.Equals("Enemy") && !immortal  && !damageShield.isActived && !protectedShield.isActived)
            Death();
    }

    override protected void Death()
    {
        // change clip to death
        audioSource.clip = deathClip;
        audioSource.outputAudioMixerGroup = _mixer.FindMatchingGroups("PlayerDeath")[0];

        // disable particles
        _particleSystem.enableEmission = false;

        //disable scripts
        _playerAttack.enabled = false;
        protectedShield.ShieldDeactivation();
        damageShield.ShieldDeactivation();

        //disable player ability to move
        StopAllCoroutines();
        GameManager.instance.GameOver();
        base.Death();
    }

    public override void InstantDestroy()
    {
        _particleSystem.enableEmission = false;
        _playerAttack.enabled = false;
        StopAllCoroutines();
        base.Death();
    }

    public void DieOfHunger()
    {
        Death();
    }

    protected override void OnEnable()
    {
        Invoke("CreatePlayerEffect", 0.01f);
        Invoke("LaterEnable", 0.35f);
    }

    void CreatePlayerEffect()
    {
        ShowDeathEffect();
    }

    void LaterEnable()
    {
        base.OnEnable();

        GameManager.instance.SetFallowers(E_FallowerType.PLAYER);
        _particleSystem.enableEmission = true;
        _playerAttack.enabled = true;

        StartCoroutine(PlayerControl());
    }
}
