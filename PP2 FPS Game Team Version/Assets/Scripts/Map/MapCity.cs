using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapCity : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject cityPrefab;
    public float spawnOffset = 20f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-spawnOffset, spawnOffset), 0, Random.Range(-spawnOffset, spawnOffset));

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        
        MapEnemyAi enemyScript = enemy.GetComponent<MapEnemyAi>();
        
        if (enemyScript != null )
        {
            enemyScript.SetHomeBase(transform);

            int randomArmySize = Random.Range(1, 21);
            enemyScript.SetArmySize(randomArmySize);
        }
    }
}
