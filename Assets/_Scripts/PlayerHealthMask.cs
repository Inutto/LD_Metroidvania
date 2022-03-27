using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.CorgiEngine;

public class PlayerHealthMask : MonoBehaviour
{

    public CanvasGroup mainGroup;
    public Character player;

    public bool canFind = false;

    // Start is called before the first frame update
    void Start()
    {
        mainGroup = GetComponent<CanvasGroup>();
        StartCoroutine(WaitForFindingPlayer());
    }

    // Update is called once per frame
    void Update()
    {

        if (canFind)
        {
            float health = player.CharacterHealth.CurrentHealth;
            float maxHealth = player.CharacterHealth.MaximumHealth;

            float ratio = health / maxHealth;

            mainGroup.alpha = 1 - ratio;
        }
        
            
    }

    public IEnumerator WaitForFindingPlayer()
    {
        yield return new WaitForSeconds(1f);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        canFind = true;
    }
}
