using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapCity : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject cityPrefab;
    public GameObject caravanPrefab;
    public Transform town;
    public float spawnOffset = 20f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();

        SpawnCaravan();
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

    void SpawnCaravan()
    {
        Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-spawnOffset, spawnOffset), 0, Random.Range(-spawnOffset, spawnOffset));
        GameObject caravan = Instantiate(caravanPrefab, spawnPosition, Quaternion.identity);

        MapCaravan caravanScript = caravan.GetComponent<MapCaravan>();

        if (caravanScript != null)
        {
            caravanScript.Initialize(transform, town);
        }
    }

    //Transform FindNearestTown()
    //{
    //    GameObject[] towns = GameObject.FindGameObjectsWithTag("Town");
    //    Transform nearest = null;
    //    float closestDistanceSqr = Mathf.Infinity;
    //    Vector3 currentPosition = transform.position;

    //    foreach (GameObject town in towns)
    //    {
    //        Vector3 directionToTown = town.transform.position - currentPosition;
    //        float distanceSqr = directionToTown.sqrMagnitude;
    //        if (distanceSqr < closestDistanceSqr)
    //        {
    //            closestDistanceSqr = distanceSqr;
    //            nearest = town.transform;
    //        }
    //    }

    //    return nearest;
    //}


}
