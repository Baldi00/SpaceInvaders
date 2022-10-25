using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource audioSource;
    public AudioClip bullet;
    public AudioClip explosion;
    public AudioClip ufo;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBullet()
    {
        audioSource.PlayOneShot(bullet);
    }
    public void PlayExplosion()
    {
        audioSource.PlayOneShot(explosion);
    }
    public void PlayUfo()
    {
        audioSource.PlayOneShot(ufo);
    }
    public void StopPlaying()
    {
        audioSource.Stop();
    }
}
