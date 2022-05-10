using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointReach : MonoBehaviour
{


    public GameObject checkpointText;

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
            // Checkpoint Reached
            checkpointText.SetActive(true);

            GetComponent<OneTimeTriggerAudio>().PlayEventAudio();
        }
    }


}
