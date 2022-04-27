using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;

public class DamageZone : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Colliders")]
    public Collider2D _collider;
    public float damageApplySpeed = 3f;
    public bool isDamage;


    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isDamage = true;

            var character = collision.gameObject.GetComponent<Character>();
            StartCoroutine(ApplyDamageToCharacter(character));

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isDamage = false;

        }
    }

    IEnumerator ApplyDamageToCharacter(Character character)
    {

        Debug.Log(character.name);

        float cumulativeDamage = 0f;
        var health = character.CharacterHealth; 
        
        while (isDamage)
        {
            cumulativeDamage += damageApplySpeed * Time.deltaTime;
            if(cumulativeDamage > 1)
            {
                health.SetHealth(health.CurrentHealth - 1, null);
                cumulativeDamage -= 1;

            }

            yield return null;
        }

        yield return null;
        
    }
}
