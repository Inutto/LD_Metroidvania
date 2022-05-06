using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{



    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    
    }


    private void OnEnable()
    {
        // Make the Door Visible
        Debug.Log("On Door Enable");
    }

    private void OnDisable()
    {
        // Make the Door Invisible
        Debug.Log("On Door Disable");
    }
}
