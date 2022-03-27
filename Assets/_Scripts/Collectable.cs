using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;




public class Collectable : MonoBehaviour
{

    public int points;
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
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
