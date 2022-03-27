using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;





public class Collectable : MonoBehaviour
{

    public int points;
    private Animator _anim;
    public float bounceOffset = 1f;
    public float bounceInterval = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        // LeanTween.moveY(gameObject, 1, 1).setEase(LeanTweenType.easeInBounce);
        StartCoroutine(BounceUp());
        

    }


    public IEnumerator BounceUp()
    {
        LeanTween.moveY(gameObject, transform.position.y + bounceOffset, bounceInterval)
            .setEase(LeanTweenType.easeInOutSine);
        yield return new WaitForSeconds(bounceInterval);
        StartCoroutine(BounceDown());
    }

    public IEnumerator BounceDown()
    {
        LeanTween.moveY(gameObject, transform.position.y - bounceOffset, bounceInterval)
            .setEase(LeanTweenType.easeInOutSine);
        yield return new WaitForSeconds(bounceInterval);
        StartCoroutine(BounceUp());
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "Player")
        {
            GameManager.Instance.AddPoints(points);
            GUIManager.Instance.RefreshPoints();

            _anim.SetTrigger("Fade");


            // Recover Health
            var health = collision.GetComponent<Character>().CharacterHealth;
            collision.GetComponent<Character>().CharacterHealth.SetHealth(health.CurrentHealth + points, null);
            

        }

    }

    public void EndFade()
    {
        Destroy(gameObject);
    }

}
