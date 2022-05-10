using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.CorgiEngine;

public class AcquireNewAbility : MonoBehaviour
{

    public GameObject abilityUI;
    



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // If Player, start fading the door
        if (collision.gameObject.tag == "Player")
        {
            // Awake the Dash Ability
            var dashAbility = collision.gameObject.GetComponent<CharacterDash>();
            dashAbility.enabled = true;

            // Give a Text
            abilityUI.SetActive(true);
        }
    }




}
