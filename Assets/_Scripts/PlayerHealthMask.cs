using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.CorgiEngine;

public class PlayerHealthMask : MonoBehaviour
{

    public CanvasGroup mainGroup;
    public Character character;
    public Text text;

    public bool canFind = false;

    // Start is called before the first frame update
    void Awake()
    {
        
        StartCoroutine(WaitForFindingPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        
        if (canFind)
        {
            float health = character.CharacterHealth.CurrentHealth;
            float maxHealth = character.CharacterHealth.MaximumHealth;

            float ratio = health / maxHealth;

            mainGroup.alpha = 1 - ratio;
        }

        

        
            
    }

    public IEnumerator WaitForFindingPlayer()
    {
        while(character == null)
        {
            yield return new WaitForSeconds(1f);
            FindCharacter();
        }
        
        canFind = true;

        if (character != null) text.text = character.name;

    }

    public void FindCharacter()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");

        foreach (var player in players)
        {
            var _character = player.GetComponent<Character>();
            if (_character != null)
            {
                character = _character;
                break;
            }
        }
    }
}
