using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapCity : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject cityPrefab;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        
        MapEnemyAi enemyScript = enemy.GetComponent<MapEnemyAi>();
        if (enemyScript != null )
        {
            enemyScript.SetHomeBase(transform);
        }
    }
}
