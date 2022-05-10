using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeTriggerAudio : MonoBehaviour
{

    public AudioClip audioClip;

    public void PlayEventAudio()
    {
        var _as = gameObject.AddComponent<AudioSource>();
        _as.clip = audioClip;
        _as.loop = false;
        _as.Play();
           
    }



}
