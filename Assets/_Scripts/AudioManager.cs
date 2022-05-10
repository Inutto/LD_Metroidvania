using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region SINGLETON
    public static AudioManager _Instance;

    public static AudioManager Instance
    {
        get
        {
            return _Instance;
        }
    }

    protected virtual void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this as AudioManager;
        }
        else
        {
            if (_Instance != this as AudioManager)
            {
                Debug.LogWarning(
                    "Multiple Instance Detected and Destroy Current Latter Instance.",
                    gameObject);
                Destroy(gameObject);
            }
        }

        // DontDestroyOnLoad(gameObject);


    }

    #endregion SINGLETON

    public AudioSource bossBGM;
    public AudioSource mainBGM;
    public AudioSource bossDefeatBGM;

    // Fade AudioSource
    public static IEnumerator FadeOutAudioSource(AudioSource audioSource, float FadeTime)
    {

        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public static IEnumerator FadeInAudioSource(AudioSource audioSource, float FadeTime)
    {
        float startVolume = 0.2f;

        audioSource.volume = 0;
        audioSource.Play();

        while (audioSource.volume < 1.0f)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.volume = 1f;
    }

    public void OnStartBoss()
    {
        StartCoroutine(FadeInAudioSource(bossBGM, 2f));
        StartCoroutine(FadeOutAudioSource(mainBGM, 2f));
    }

    public void OnEndBoss()
    {
        StartCoroutine(FadeInAudioSource(bossDefeatBGM, 0f));
        StartCoroutine(FadeOutAudioSource(bossBGM, 0.3f));
        GetComponent<OneTimeTriggerAudio>().PlayEventAudio();
        
    }

    IEnumerator AfterEndBoss()
    {
        yield return new WaitForSeconds(5.0f);
        StartCoroutine(FadeInAudioSource(mainBGM, 2f));
        StartCoroutine(FadeOutAudioSource(bossDefeatBGM, 2f));
    }

    public void OnFailBoss()
    {
        StartCoroutine(FadeInAudioSource(mainBGM, 2f));
        StartCoroutine(FadeOutAudioSource(bossBGM, 2f));
    }
    
}
