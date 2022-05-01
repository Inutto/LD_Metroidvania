using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VerticalMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public float bounceOffset = 1f;
    public float bounceInterval = 1f;

    void Start()
    {


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
}
