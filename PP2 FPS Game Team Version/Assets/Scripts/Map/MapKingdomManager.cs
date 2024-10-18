using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapKingdomManager : MonoBehaviour
{
    public static MapKingdomManager Instance { get; private set; }

    public List<Transform> townsInDwarfKingdom = new List<Transform>();
    public List<Transform> townsInOgreKingdom = new List<Transform>();
    public List<Transform> townsInElfKingdom = new List<Transform>();
    public List<Transform> townsInHumanKingdom = new List<Transform>();

    public List<Transform> citiesInDwarfKingdom = new List<Transform>();
    public List<Transform> citiesInOgreKingdom = new List<Transform>();
    public List<Transform> citiesInElfKingdom = new List<Transform>();
    public List<Transform> citiesInHumanKingdom = new List<Transform>();

    public Material dwarfMaterial;
    public Material ogreMaterial;
    public Material elfMaterial;
    public Material humanMaterial;

    public float mapMinX = -100f;
    public float mapMaxX = 100f;
    public float mapMinZ = -100f;
    public float mapMaxZ = 100f;

    private bool kingdomsAssigned = false;


    public enum Kingdom
    {
        Dwarves,
        Ogres,
        Elves,
        // Player kingdom
        Humans
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        Collider mapCollider = GameObject.FindGameObjectWithTag("Map").GetComponent<Collider>();

        if (mapCollider != null)
        {
            Bounds mapBounds = mapCollider.bounds;

            mapMinX = mapBounds.min.x;
            mapMaxX = mapBounds.max.x;
            mapMinZ = mapBounds.min.z;
            mapMaxZ = mapBounds.max.z;
        }

        if (!kingdomsAssigned)
        {
            AssignTownsAndCitiesToKingdoms();
            kingdomsAssigned = true;
        }
    }
    void AssignTownsAndCitiesToKingdoms()
    {
        GameObject[] towns = GameObject.FindGameObjectsWithTag("Town");
        GameObject[] cities = GameObject.FindGameObjectsWithTag("City");

        foreach (GameObject town in towns)
        {
            Vector3 position = town.transform.position;
            AssignToKingdom(position, town.transform, isCity: false);
        }

        foreach (GameObject city in cities)
        {
            Vector3 position = city.transform.position;
            AssignToKingdom(position, city.transform, isCity: true);
        }

        //Debug.Log($"Dwarves Kingdom has {townsInDwarfKingdom.Count} towns.");
        //Debug.Log($"Ogres Kingdom has {townsInOgreKingdom.Count} towns.");
        //Debug.Log($"Elves Kingdom has {townsInElfKingdom.Count} towns.");
        //Debug.Log($"Humans Kingdom has {townsInHumanKingdom.Count} towns.");
    }

    void AssignToKingdom(Vector3 position, Transform objectTransform, bool isCity)
    {
        if (position.x < 0 && position.z > 0)
        {
            // Dwarves Kingdom (Top left)
            if (isCity)
            {
                citiesInDwarfKingdom.Add(objectTransform);
                UpdateCityAppearance(objectTransform, Kingdom.Dwarves);
            }
            else townsInDwarfKingdom.Add(objectTransform);
        }
        else if (position.x >= 0 && position.z > 0)
        {
            // Ogres Kingdom (Top right)
            if (isCity)
            {
                citiesInOgreKingdom.Add(objectTransform);
                UpdateCityAppearance(objectTransform, Kingdom.Ogres);
            }
        else townsInOgreKingdom.Add(objectTransform);
        }
        else if (position.x < 0 && position.z <= 0)
        {
            // Elves Kingdom (Bottom left)
            if (isCity)
            {
                citiesInElfKingdom.Add(objectTransform);
                UpdateCityAppearance(objectTransform, Kingdom.Elves);
            }
            else townsInElfKingdom.Add(objectTransform);
        }
        else if (position.x >= 0 && position.z <= 0)
        {
            // Humans Kingdom (Bottom right)
            if (isCity)
            {
                citiesInHumanKingdom.Add(objectTransform);
                UpdateCityAppearance(objectTransform, Kingdom.Humans);
            }
            else townsInHumanKingdom.Add(objectTransform);
        }
    }

    public void CaptureCity(Transform city, Kingdom newKingdom)
    {
        // Remove the city from current kingdom
        RemoveCityFromKingdom(city);

        // Add the city to player kingdom(human)
        AddCityToKingdom(city, newKingdom);

        // Update city mat
        UpdateCityAppearance(city, newKingdom);
    }
    public void CaptureCityForHumanKingdom(Transform city)
    {
        if (city == null)
        {
            Debug.LogError("CaptureCityForHumanKingdom: City is null!");
            return;
        }
        Debug.Log("Capturing city for human kingdom: " + city.name);
        CaptureCity(city, Kingdom.Humans);
    }
    void RemoveCityFromKingdom(Transform city)
    {
        if (citiesInDwarfKingdom.Contains(city))
        {
            citiesInDwarfKingdom.Remove(city);
        }
        else if (citiesInOgreKingdom.Contains(city))
        {
            citiesInOgreKingdom.Remove(city);
        }
        else if (citiesInElfKingdom.Contains(city))
        {
            citiesInElfKingdom.Remove(city);
        }
        else if (citiesInHumanKingdom.Contains(city))
        {
            citiesInHumanKingdom.Remove(city);
        }
    }

    void AddCityToKingdom(Transform city, Kingdom newKingdom)
    {
        switch (newKingdom)
        {
            case Kingdom.Dwarves:
                citiesInDwarfKingdom.Add(city);
                break;
            case Kingdom.Ogres:
                citiesInOgreKingdom.Add(city);
                break;
            case Kingdom.Elves:
                citiesInElfKingdom.Add(city);
                break;
            case Kingdom.Humans:
                citiesInHumanKingdom.Add(city);
                break;
        }
    }

    public void UpdateCityAppearance(Transform city, Kingdom kingdom)
    {
        Renderer[] renderers = city.GetComponentsInChildren<Renderer>();
        Debug.Log($"Updating material for city: {city.name} to {kingdom}");

        foreach (Renderer renderer in renderers)
        {
            renderer.material = new Material(GetMaterialForKingdom(kingdom));
            Debug.Log($"Assigned new material to renderer in city {city.name}");
        }
    }

    public Material GetMaterialForKingdom(Kingdom kingdom)
    {
        switch (kingdom)
        {
            case Kingdom.Dwarves:
                return dwarfMaterial;
            case Kingdom.Ogres:
                return ogreMaterial;
            case Kingdom.Elves:
                return elfMaterial;
            case Kingdom.Humans:
                return humanMaterial;
            default:
                Debug.LogError($"Unknown kingdom: {kingdom}. Returning default material.");
                return humanMaterial;
        }
    }

    public Transform GetNearestCity(Vector3 playerPosition)
    {
        List<Transform> allCities = new List<Transform>();
        allCities.AddRange(citiesInDwarfKingdom);
        allCities.AddRange(citiesInOgreKingdom);
        allCities.AddRange(citiesInElfKingdom);
        allCities.AddRange(citiesInHumanKingdom);

        Transform nearestCity = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Transform city in allCities)
        {
            float distanceToCity = Vector3.Distance(playerPosition, city.position);
            if (distanceToCity < nearestDistance)
            {
                nearestDistance = distanceToCity;
                nearestCity = city;
            }
        }
        if (nearestCity != null)
        {
            Debug.Log("Nearest city to player is: " + nearestCity.name);
        }
        else
        {
            Debug.LogError("No city found near player.");
        }
        return nearestCity;
    }


    public Transform GetRandomTownFromKingdom(string kingdom)
    {
        List<Transform> townList = null;

        switch (kingdom)
        {
            case "Dwarves":
                townList = townsInDwarfKingdom;
                break;
            case "Ogres":
                townList = townsInOgreKingdom;
                break;
            case "Elves":
                townList = townsInElfKingdom;
                break;
            case "Humans":
                townList = townsInHumanKingdom;
                break;
        }

        if (townList == null || townList.Count == 0)
        {
       //   Debug.LogWarning($"No towns found for kingdom: {kingdom}");
            return null;
        }

        // Debug number of available towns for this kingdom
        // Debug.Log($"Number of towns in {kingdom}: {townList.Count}");

        // Return a random town
        return townList[Random.Range(0, townList.Count)];
    }

    public Transform GetRandomCityFromKingdom(string kingdom)
    {
        List<Transform> cityList = null;

        switch (kingdom)
        {
            case "Dwarves":
                cityList = citiesInDwarfKingdom;
                break;
            case "Ogres":
                cityList = citiesInOgreKingdom;
                break;
            case "Elves":
                cityList = citiesInElfKingdom;
                break;
            case "Humans":
                cityList = citiesInHumanKingdom;
                break;
        }

        if (cityList == null || cityList.Count == 0)
        {
            return null;
        }

        return cityList[Random.Range(0, cityList.Count)];
    }

    public List<Transform> GetCityListForKingdom(string kingdom)
    {
        switch (kingdom)
        {
            case "Dwarves":
                return citiesInDwarfKingdom;
            case "Ogres":
                return citiesInOgreKingdom;
            case "Elves":
                return citiesInElfKingdom;
            case "Humans":
                return citiesInHumanKingdom;
            default:
                Debug.LogError($"Kingdom '{kingdom}' not recognized.");
                return null;
        }
    }

    public bool IsCityInHumanKingdom(Transform city)
    {
        return citiesInHumanKingdom.Contains(city);
    }
    public class CityInfo
    {
        public Transform cityTransform;
        public GameObject combatScenePrefab;
    }


}
