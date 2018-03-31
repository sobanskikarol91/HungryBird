using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerAttack : MonoBehaviour
{
    public AudioClip noAmmoClip;
    public AudioMixer _mixer;
    AudioSource _audioSource;
    public GameObject egg;
    public Transform eggSpawn;
    public float coolDown = 0.5f;

    int eggsAmount;
    int EggsAmount  { get { return eggsAmount; } set { eggsAmount = value; GameManager.instance.eggText.text = value.ToString();} }
    float coolDownLeft;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if(Application.platform == RuntimePlatform.Android)
        GameObject.FindGameObjectWithTag("joystick2").GetComponent<AttackButton>()._playerAttack = this;
        coolDownLeft = coolDown;
    }

    private void Start()
    {
        EggsAmount = GameManager.instance.playerStartEggs;
    }

    IEnumerator Control()
    {
        while(true)
        {
            if (Input.GetKeyDown(KeyCode.Space) && coolDown > 0 && eggsAmount > 0  && Time.timeScale != 0.0f)
                DropEgg();
            else if(Input.GetKeyDown(KeyCode.Space))
            {
                _audioSource.Play();  
            }

            coolDownLeft -= Time.deltaTime;
            yield return null;
        }
    }

    public void DropEgg() // Attack Button
    {
        Instantiate(egg, eggSpawn.position, Quaternion.identity);
        EggsAmount--;
    }

    public void AddAmmo(int amount)
    {
        EggsAmount += amount;
    }

    private void OnEnable()
    {
        _audioSource.clip = noAmmoClip;
        _audioSource.outputAudioMixerGroup = _mixer.FindMatchingGroups("NoAmmo")[0];
        EggsAmount = GameManager.instance.playerStartEggs;

        //Device Detected
        if (!SystemInfo.supportsAccelerometer)
            StartCoroutine(Control());
    }
}
