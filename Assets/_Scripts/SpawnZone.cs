using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{

    private Collider2D triggerArea;
    [Header("Enemy Prefab")]
    public GameObject enemy;


    [Header("Spawn Position")]
    public Transform[] spawnPosition;
    public GameObject[] enemies;


    [Header("Debug")]
    public bool drawGizmos;
    public Color GizmosZoneColor;
    public Color GizmosObjectColor;

    protected Vector3 _gizmoSize;
    public float drawZ;
    public float drawObjectRadius;

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

#if UNITY_EDITOR
    /// <summary>
    /// Draws gizmos to show the shape of the zone
    /// </summary>
    protected virtual void OnDrawGizmos()
    {
        if (!drawGizmos)
        {
            return;
        }


        var _triggerArea = GetComponent<Collider2D>();

        // Draw The Cube Area
        Gizmos.color = GizmosZoneColor;


        _gizmoSize.x = _triggerArea.bounds.size.x;
        _gizmoSize.y = _triggerArea.bounds.size.y;
        _gizmoSize.z = drawZ;
        Gizmos.DrawCube(_triggerArea.bounds.center, _gizmoSize);


        Gizmos.color = GizmosObjectColor;
        var _childCount = transform.childCount;
        for(int i = 0; i< _childCount; ++i)
        {
            var child = transform.GetChild(i);
            Gizmos.DrawSphere(child.position, drawObjectRadius);
        }
        
        
    }
#endif
}
