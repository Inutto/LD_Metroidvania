using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;
using MoreMountains.Feedbacks;

public class OneWayGate : MonoBehaviour
{


    private BoxCollider2D triggerCollider;
    private BoxCollider2D mainCollider;
    private Animator _anim;
    private AudioSource _as;

    public MMFeedbacks gateOpenSound;


    private GameObject _gate;

    // Start is called before the first frame update
    void Start()
    {
        mainCollider = GetComponentInChildren<BoxCollider2D>();
        triggerCollider = GetComponent<BoxCollider2D>();
        _anim = GetComponent<Animator>();
        _as = GetComponent<AudioSource>();

        _gate = transform.GetChild(0).gameObject;

        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        // If Player, start fading the door
        if (collision.gameObject.tag == "Player")
        {
            // Disable the trigger first
            triggerCollider.enabled = false;

            _anim.SetTrigger("Open");

            // Play the Audio
            // _as.Play();
            gateOpenSound?.PlayFeedbacks();

            // Delete the gameobject when audio is finished
            // StartCoroutine(WaitForAudio());
        }
    }

    public void OnGateFade()
    {

        // Disable Main Collider so we can pass now
        _gate.SetActive(false);

    }

    public IEnumerator WaitForAudio()
    {
        yield return new WaitUntil(() => _as.isPlaying == false);
        if (gameObject != null) gameObject.SetActive(false);
    }


}
