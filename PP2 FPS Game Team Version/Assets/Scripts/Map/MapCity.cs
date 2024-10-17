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

        SpawnCaravansAtRandomCitiesOnce();
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

    //void SpawnCaravan()
    //{
    //    //Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-spawnOffset, spawnOffset), 0, Random.Range(-spawnOffset, spawnOffset));
    //    //GameObject caravan = Instantiate(caravanPrefab, spawnPosition, Quaternion.identity);

    //    //MapCaravan caravanScript = caravan.GetComponent<MapCaravan>();

    //    //if (caravanScript != null)
    //    //{
    //    //    string cityKingdom = GetCityKingdom(transform);
    //    //    Debug.Log($"City at {transform.position} belongs to kingdom: {cityKingdom}");

    //    //    Transform randomTown = kingdomManager.GetRandomTownFromKingdom(cityKingdom);

    //    //    // check if a valid town was found
    //    //    if (randomTown != null)
    //    //    {
    //    //        Debug.Log($"Initializing caravan at {transform.position} to travel to {randomTown.position} in kingdom {cityKingdom}");
    //    //        caravanScript.Initialize(transform, randomTown);
    //    //    }
    //    //    else
    //    //    {
    //    //        // Handle the case when there is no town found
    //    //        Debug.LogError($"No valid towns found in the {cityKingdom} kingdom for caravan to initialize.");
    //    //        Destroy(caravan);
    //    //    }

    //    //}
    //    // NEW BELOW

    //    // Create a list of all kingdom names
    //    string[] kingdoms = { "Dwarves", "Ogres", "Elves", "Humans" };

    //    foreach (string kingdom in kingdoms)
    //    {
    //        // Get all towns for current kingdom
    //        Transform randomTown = kingdomManager.GetRandomTownFromKingdom(kingdom);

    //        if (randomTown != null)
    //        {
    //            // Randomize the spawn pos for the caravan
    //            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-spawnOffset, spawnOffset), 0, Random.Range(-spawnOffset, spawnOffset));
    //            GameObject caravan = Instantiate(caravanPrefab, spawnPosition, Quaternion.identity);

    //            // Get caravan script and intialize
    //            MapCaravan caravanScript = caravan.GetComponent<MapCaravan>();
    //            if (caravanScript != null)
    //            {
    //                Debug.Log($"Spawning caravan in {kingdom} kingdom, travelling to {randomTown.position}");
    //                caravanScript.Initialize(transform, randomTown);
    //            }
    //        }
    //        else
    //        {
    //            Debug.LogError($"No towns found for {kingdom} kingdom. Caravan not spawned.");
    //        }
    //    }
    //}

    void SpawnCaravansAtRandomCitiesOnce()
    {
        // List of all kingdom names
        string[] kingdoms = { "Dwarves", "Ogres", "Elves", "Humans" };

        foreach (string kingdom in kingdoms)
        {
            // Get a random city in the kingdom
            Transform randomCity = kingdomManager.GetRandomTownFromKingdom(kingdom);

            if (randomCity != null)
            {
                // Spawn a caravan at the random city
                SpawnCaravan(randomCity, kingdom);
            }
            else
            {
                Debug.LogWarning($"No cities found for {kingdom} kingdom. Cannot spawn caravan.");
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

public void ManuallySpawnCaravan(string kingdom)
    {
        Transform randomCity = kingdomManager.GetRandomTownFromKingdom(kingdom);

        if (randomCity != null)
        {
            SpawnCaravan(randomCity, kingdom);
        }
        else
        {
            Debug.LogWarning($"No cities found for {kingdom}. Cannot spawn caravan manually.");
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
