using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SoundManager>();
            }

            return _instance;
        }
    }

    public AudioClip ahhh;
    public AudioClip scream;
    public AudioClip ouch;
    public AudioClip groundHit;
    public AudioClip startGame;
    public AudioClip altBeep;
    public AudioClip menuClick;

    public AudioSource sfxSource;
    public AudioSource musicSource;
    public AudioSource windSource;

    private IEnumerator altIntervalCoroutine;

    public void PlayMusic(AudioClip clip, bool loop)
    {
        musicSource.loop = loop;
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        sfxSource.PlayOneShot(clip, volume);
    }

    public void RunPlayAltInterval()
    {
        if (altIntervalCoroutine == null)
        {
            altIntervalCoroutine = PlayAltPerInterval();
            StartCoroutine(altIntervalCoroutine);
        }
    }

    private IEnumerator PlayAltPerInterval()
    {
        if (GameManager.Instance == null)
            yield break;

        while (GameManager.Instance.IsGameRunning())
        {
            yield return new WaitForSeconds(GameManager.Instance.GetPlayerToGroundPerc() * 2.5f);

            SoundManager.Instance.PlaySFX(SoundManager.Instance.altBeep, 0.25f);
        }

        altIntervalCoroutine = null;
    }
}
