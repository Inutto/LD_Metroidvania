using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeTriggerAudio : MonoBehaviour
{

    public AudioClip audioClip;
    bool hasPlayed = false;

    public GameObject checkpointText;

    public void PlayEventAudio()
    {
        if (hasPlayed) return;

        hasPlayed = true;
        var _as = AudioManager.Instance.gameObject.AddComponent<AudioSource>();
        _as.clip = audioClip;
        _as.loop = false;
        _as.Play(); 

        checkpointText.SetActive(true);

    }



}
