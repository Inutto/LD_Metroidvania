using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{


    public Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Player1_Interact"))
        {
            // Attack Once
            _anim.SetTrigger("Attack");
        }
    }

    public void EndAnimation()
    {
        _anim.SetTrigger("End");
    }
}
