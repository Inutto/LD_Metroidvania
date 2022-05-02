using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{

    public enum SpawnType {ENEMY, PICKUP, BOSS};

    private Collider2D triggerArea;
    [Header("Prefab")]
    public GameObject enemy;
    public GameObject pickup;
    public GameObject boss;


    [Header("Boss Specific")]
    public GameObject bossInstance;
    public List<GameObject> wallList;


    [Header("Spawn Position")]
    public Transform[] spawnPosition;
    public List<GameObject> spawnsList = new List<GameObject>();
    public Dictionary<Transform, string> spawnInfoDic = new Dictionary<Transform, string>();


    [Header("Debug")]
    public bool drawGizmos;
    public Color GizmosZoneColor;
    public Color enemyColor;
    public Color pickupColor;

    protected Vector3 _gizmoSize;
    public float drawZ;
    public float drawObjectRadius;

    // Start is called before the first frame update
    void Start()
    {
        triggerArea = GetComponent<Collider2D>();
        InitiateSpawnInfoDic();
    }


    private void InitiateSpawnInfoDic()
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; ++i)
        {
            var spawnTrans = transform.GetChild(i);
            var spawnType = spawnTrans.tag;
            spawnInfoDic[spawnTrans] = spawnType;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            foreach(var info in spawnInfoDic)
            {
                // Initialize Info
                var trans = info.Key;
                var type = info.Value;
                GameObject prefab = null;

                // Switch
                if(type == "Enemy")
                {
                    prefab = enemy;
                } else if (type == "Pickup")
                {
                    prefab = pickup;
                } else if (type == "Boss")
                {
                    prefab = boss;
                }

                // Inst
                if(prefab != null)
                {
                    var newSpawn = Instantiate(
                            prefab,
                            trans.position,
                            trans.rotation);

                    if(type == "Boss")
                    {
                        // Track boss stats
                        OnBossStart(newSpawn);
                        StartCoroutine(WaitUntilBossEnd());
                    }

                    spawnsList.Add(newSpawn);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            foreach(var spawn in spawnsList)
            {
                if (spawn != null) Destroy(spawn);
            }
            spawnsList.Clear();
        }
    }

    public void OnBossStart(GameObject _bossInstance)
    {
        bossInstance = _bossInstance;
        foreach(var wall in wallList)
        {
            wall.SetActive(true);
        }
    }

    public IEnumerator WaitUntilBossEnd()
    {

        // I really wish to add the listen at Enemy's end, but...
        while (bossInstance.activeSelf != false)
        {
            yield return null;
        }

        // End
        foreach (var wall in wallList)
        {
            wall.SetActive(false);
        }
        yield return null;



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

        // Draw The Spawn Area

        if(spawnInfoDic == null) InitiateSpawnInfoDic();

        foreach (var info in spawnInfoDic)
        {
            var trans = info.Key;
            var type = info.Value;
            if(type == "Enemy")
            {
                Gizmos.color = enemyColor;
            } else if(type == "Pickup")
            {
                Gizmos.color = pickupColor;
            }

            Gizmos.DrawSphere(trans.position, drawObjectRadius);
        }



        
        
    }
#endif
}
