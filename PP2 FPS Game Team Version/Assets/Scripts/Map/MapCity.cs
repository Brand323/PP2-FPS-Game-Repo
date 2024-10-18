using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCity : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject cityPrefab;
    public GameObject caravanPrefab;
    public float spawnOffset = 20f;
    private MapKingdomManager kingdomManager;

    public bool caravanSpawned = false;
    private bool isCaptured = false;

    // Start is called before the first frame update
    void Start()
    {
        kingdomManager = MapKingdomManager.Instance;

        if (!kingdomManager.IsCityInHumanKingdom(transform))
        {
            SpawnEnemy();
        }
        else
        {
            UpdateCityAppearance();
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
            MapKingdomManager.Kingdom kingdom = GetCityKingdom(transform);

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
   
    void SpawnCaravan(Transform city, MapKingdomManager.Kingdom kingdom)
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
            Transform randomTown = kingdomManager.GetRandomTownFromKingdom(kingdom.ToString());

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
    public void SpawnCaravanFromNearestCity(Transform playerTransform)
    {
        List<Transform> allCities = new List<Transform>();
        allCities.AddRange(kingdomManager.citiesInDwarfKingdom);
        allCities.AddRange(kingdomManager.citiesInOgreKingdom);
        allCities.AddRange(kingdomManager.citiesInElfKingdom);
        allCities.AddRange(kingdomManager.citiesInHumanKingdom);

        Transform nearestCity = null;
        float nearestDistance = Mathf.Infinity;

        // Finds the nearest city to the player
        foreach (Transform city in allCities)
        {
            float distanceToPlayer = Vector3.Distance(city.position, playerTransform.position);

            if (distanceToPlayer < nearestDistance)
            {
                nearestDistance = distanceToPlayer;
                nearestCity = city;
            }
        }

        if (nearestCity != null && !caravanSpawned)
        {
            //Checks kingdom
            MapKingdomManager.Kingdom kingdom = GetCityKingdom(nearestCity);

            if (kingdom != null)
            {
                SpawnCaravan(nearestCity, kingdom);
                caravanSpawned = true;
            }
        }
        else
        {
            Debug.LogWarning("No valid nearest city found or caravan already spawned.");
        }
    }

    public MapKingdomManager.Kingdom GetCityKingdom(Transform city)
    {

        if (kingdomManager.IsCityInHumanKingdom(city))
        {
            return MapKingdomManager.Kingdom.Humans;
        }
        else if (kingdomManager.citiesInDwarfKingdom.Contains(city))
        {
            return MapKingdomManager.Kingdom.Dwarves;
        }
        else if (kingdomManager.citiesInOgreKingdom.Contains(city))
        {
            return MapKingdomManager.Kingdom.Ogres;
        }
        else if (kingdomManager.citiesInElfKingdom.Contains(city))
        {
            return MapKingdomManager.Kingdom.Elves;
        }

        return MapKingdomManager.Kingdom.Humans;
    }

    public void CaptureCityByPlayer()
    {
        MapKingdomManager.Instance.CaptureCityForHumanKingdom(transform);
        isCaptured = true;
    }

    void UpdateCityAppearance()
    {
        if (isCaptured || kingdomManager.IsCityInHumanKingdom(transform))
        {
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                renderer.material = new Material(kingdomManager.humanMaterial);
            }
        }
        else
        {
            kingdomManager.UpdateCityAppearance(transform, GetCityKingdom(transform));
        }
    }

    //Test for Caravan Spawn val will trigger method with quest menu in the future
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("MapPlayer"))
    //    {
    //        SpawnCaravanFromNearestCity(other.transform);
    //    }
    //}
}
