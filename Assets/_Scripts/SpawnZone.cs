using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;
using UnityEngine.UI;

public class SpawnZone : MonoBehaviour
{

    public enum SpawnType {ENEMY, PICKUP, BOSS};

    private Collider2D triggerArea;
    [Header("Prefab")]
    public GameObject enemy;
    public GameObject pickup;
    public GameObject boss;


    [Header("Boss Specific")]
    public bool isBossRoom;
    public GameObject bossInstance;
    public GameObject playerInstance;
    public List<GameObject> wallList;
    public GameObject bossText;
    public string bossTextContent;
    


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
            // If Already exit a boss, ignore all
            if (bossInstance != null) return;

            // Add Player Instance to track death
            playerInstance = collision.gameObject;
            playerInstance.GetComponent<Health>().OnDeath += ResetZone;

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

            // If Boss Still Alive, don't kill the boss because leave the zone
            if (bossInstance != null) return;

            ResetZone();
        }
    }

    void ResetZone()
    {
        // Clear Spawns
        foreach (var spawn in spawnsList)
        {
            if (spawn != null) Destroy(spawn);
        }
        spawnsList.Clear();

        // Clear Walls
        DisableWalls();

        if (isBossRoom)
        {
            AudioManager.Instance.OnFailBoss();
        }
        
    }


    public void OnBossStart(GameObject _bossInstance)
    {
        if (bossInstance != null) return; 
        bossInstance = _bossInstance;

        AudioManager.Instance.OnStartBoss();
        EnableWalls();
    }

    public IEnumerator WaitUntilBossEnd()
    {
        Debug.Log("Wait for Boss dead");
        yield return new WaitUntil(() => 
            bossInstance?.GetComponent<Character>().CharacterHealth.CurrentHealth <= 0);


        Debug.Log("Boss has been defeated");


        // End
        AudioManager.Instance.OnEndBoss();
        DisableWalls();

        // Show The BossText UI
        if (bossText != null)
        {
            Debug.Log("Set Boss Text True");
            bossText.SetActive(true);
            UpdateTextContent(bossText);
        } else
        {
            Debug.Log("Null Boss Reference");
        }


        // Disable this zone
        gameObject.SetActive(false);

    }

    void UpdateTextContent(GameObject targetObject)
    {
        var textComponent = targetObject.GetComponent<Text>();
        textComponent.text = bossTextContent;
    }

    void DisableWalls()
    {
        foreach (var wall in wallList)
        {
            if(wall != null) wall.SetActive(false);
        }
    }

    void EnableWalls()
    {
        foreach (var wall in wallList)
        {
            if (wall != null) wall.SetActive(true);
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
