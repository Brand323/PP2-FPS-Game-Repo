using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapCity : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject cityPrefab;
    public GameObject caravanPrefab;
    public float spawnOffset = 20f;
    private MapKingdomManager kingdomManager;

    // Start is called before the first frame update
    void Start()
    {
        kingdomManager = FindObjectOfType<MapKingdomManager>();

        if (!kingdomManager.IsCityInHumanKingdom(transform))
        {
            SpawnEnemy();
        }

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
            string cityKingdom = GetCityKingdom(transform);

            Transform randomTown = kingdomManager.GetRandomTownFromKingdom(cityKingdom);

            caravanScript.Initialize(transform, randomTown);
        }
    }

    string GetCityKingdom(Transform city)
    {
        if (kingdomManager.IsCityInHumanKingdom(city))
        {
            return "Humans";
        }
        else if (kingdomManager.citiesInDwarfKingdom.Contains(city))
        {
            return "Dwarves";
        }
        else if (kingdomManager.citiesInOgreKingdom.Contains(city))
        {
            return "Ogres";
        }
        else if (kingdomManager.citiesInElfKingdom.Contains(city))
        {
            return "Elves";
        }

        return null;
    }

}
