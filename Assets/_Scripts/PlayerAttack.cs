using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{


    public Animator _anim;
    public float coldDown = 0.5f;
    public bool canAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Player1_Interact") && canAttack)
        {
            StartCoroutine(AttackColdDown());
            // Attack Once
            _anim.SetTrigger("Attack");
        }
    }

    public void EndAnimation()
    {
        _anim.SetTrigger("End");
    }

    public IEnumerator AttackColdDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(coldDown);
        canAttack = true;
    }
}
