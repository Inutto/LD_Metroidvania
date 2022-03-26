using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{


    private Collider2D triggerArea;
    [Header("Enemy Prefab")]
    public GameObject enemy;


    [Header("Spawn Position")]
    public Transform[] spawnPosition;
    public GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        triggerArea = GetComponent<Collider2D>();
        InitiateSpawnPos();
        InitiateEnemies();
    }


    private void InitiateSpawnPos()
    {
        int childCount = transform.childCount;
        spawnPosition = new Transform[childCount];
        for(int i = 0; i < childCount; ++i)
        {
            spawnPosition[i] = transform.GetChild(i);
        }

        
    }

    private void InitiateEnemies()
    {
        int childCount = transform.childCount;
        enemies = new GameObject[childCount];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            for(int i = 0; i < spawnPosition.Length; ++i)
            {
                enemies[i] = Instantiate(
                    enemy,
                    spawnPosition[i].position,
                    spawnPosition[i].rotation);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            for (int i = 0; i < spawnPosition.Length; ++i)
            {
                Destroy(enemies[i]);
                enemies[i] = null;
            }
        }
    }
}
