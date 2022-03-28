using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    private Collider2D _collider;
    private SpriteRenderer _spriteRender;
    private bool hasTriggered = false;

    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _spriteRender = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered && collision.tag == "Player")
        {
            _spriteRender.flipX = true;
            hasTriggered = true;
        }
    }
}
