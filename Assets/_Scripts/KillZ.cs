using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZ : MonoBehaviour
{

    public float limitY;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(KillWhenLowerThanLimit());
    }

    public IEnumerator KillWhenLowerThanLimit()
    {
        // Condition
        while(transform.position.y >= limitY)
        {
            yield return null;
        }

        // Action
        Destroy(gameObject);
        yield return null;
    }
}
