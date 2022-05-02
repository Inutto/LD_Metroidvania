using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZone : MonoBehaviour
{




    [Header("prefabs")]
    public GameObject boss;
    public Transform spawnPosition;
    public List<GameObject> walls;

    [Header("In Game Stats")]
    private GameObject bossInstance;

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
        if(collision.tag == "Player")
        {

        }
    }







}
