using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class EggController : MonoBehaviour
{
    public AudioClip crashClip;
    public AudioMixer _audioMixer;
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    Collider2D colldier2d;
   

    private void Start()
    {      
        audioSource = GetComponent<AudioSource>();
        Destroy(gameObject, audioSource.clip.length);
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        colldier2d = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Enemy"))
        {
            audioSource.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("CrashEgg")[0];
            audioSource.clip = crashClip;
            audioSource.Play();
            Destroy(gameObject, audioSource.clip.length);
            spriteRenderer.enabled = false;
            colldier2d.enabled = false;
        }
    }
}
