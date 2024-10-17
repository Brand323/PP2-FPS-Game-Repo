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

    //Hashset to track the cities that have already spawned Caravans
    private HashSet<Transform> citiesThatSpawnedCaravan = new HashSet<Transform>();

    private bool caravanSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        kingdomManager = FindObjectOfType<MapKingdomManager>();

        if (!kingdomManager.IsCityInHumanKingdom(transform))
        {
            SpawnEnemy();
        }
        if (!caravanSpawned)
        {
            SpawnCaravansForThisCity();
        }
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

    void SpawnCaravansForThisCity()
    {
        // Make sure the caravan spawns only once for this city
        if (!caravanSpawned)
        {
            // Get the kingdom of the current city
            string kingdom = GetCityKingdom(transform);

            // If the kingdom is valid, spawn a caravan
            if (kingdom != null)
            {
                SpawnCaravan(transform, kingdom);

                // Mark the caravan as spawned for this city
                caravanSpawned = true;
            }
            else
            {
                Debug.LogWarning($"Kingdom for city at {transform.position} could not be determined.");
            }
        }
    }
   
    void SpawnCaravan(Transform city, string kingdom)
    {
        // Randomize spawn position around the city
        Vector3 spawnPosition = city.position + new Vector3(Random.Range(-spawnOffset, spawnOffset), 0, Random.Range(-spawnOffset, spawnOffset));

        // Instantiate the caravan prefab
        GameObject caravan = Instantiate(caravanPrefab, spawnPosition, Quaternion.identity);

        // Get the caravan script
        MapCaravan caravanScript = caravan.GetComponent<MapCaravan>();

        if (caravanScript != null)
        {
            // Get a random town for the caravan to travel to
            Transform randomTown = kingdomManager.GetRandomTownFromKingdom(kingdom);

            if (randomTown != null)
            {
             //   Debug.Log($"Spawning caravan from {city.position} in {kingdom} to {randomTown.position}");
                caravanScript.Initialize(city, randomTown);
            }
            else
            {
            //    Debug.LogWarning($"No towns found in {kingdom} for caravan to travel to.");
                Destroy(caravan); // Optionally destroy the caravan if no valid town is found
            }
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
