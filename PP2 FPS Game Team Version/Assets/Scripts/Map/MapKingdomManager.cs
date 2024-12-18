using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MapKingdomManager : MonoBehaviour
{
    public static MapKingdomManager instance;
    public List<Transform> townsInDwarfKingdom = new List<Transform>();
    public List<Transform> townsInOgreKingdom = new List<Transform>();
    public List<Transform> townsInElfKingdom = new List<Transform>();
    public List<Transform> townsInHumanKingdom = new List<Transform>();

    public List<Transform> allCities = new List<Transform>();
    public List<Transform> citiesInDwarfKingdom = new List<Transform>();
    public List<Transform> citiesInOgreKingdom = new List<Transform>();
    public List<Transform> citiesInElfKingdom = new List<Transform>();
    public List<Transform> citiesInHumanKingdom = new List<Transform>();

    public float mapMinX = -100f;
    public float mapMaxX = 100f;
    public float mapMinZ = -100f;
    public float mapMaxZ = 100f;
    [SerializeField] float citySpawnOffset = 25f;

    public ClickMove mapPlayer;

    public Transform currentCity;
    public Transform CurrentCity
    {
        get { return currentCity; }
        set { currentCity = value; }
    }

    public GameObject cityPrefab;
    public bool playerWon;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        mapPlayer = GameObject.FindObjectOfType<ClickMove>();
        //if (currentCity != null)
        //{
        //    Destroy(currentCity);
        //}
        //else 
        //{
        //    findNearestCity();
        //    DontDestroyOnLoad(currentCity);
        //}
        Collider mapCollider = GameObject.FindGameObjectWithTag("Map").GetComponent<Collider>();
        if (mapCollider != null)
        {
            Bounds mapBounds = mapCollider.bounds;

            mapMinX = mapBounds.min.x + 130;
            mapMaxX = mapBounds.max.x - 130;
            mapMinZ = mapBounds.min.z + 130;
            mapMaxZ = mapBounds.max.z - 130;

            mapMinX += citySpawnOffset;
            mapMaxX -= citySpawnOffset;
            mapMinZ += (citySpawnOffset*2);
            mapMaxZ -= (citySpawnOffset*2);
        }
    }

    private void Update()
    {
        if(mapPlayer == null)
        {
            mapPlayer = GameObject.FindObjectOfType<ClickMove>();
        }
        if (SceneManager.GetActiveScene().name == "Map Scene")
        {
            foreach (Transform city in citiesInHumanKingdom)
            {
                city.gameObject.SetActive(true);
            }
            foreach (Transform city in allCities)
            {
                city.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform city in citiesInHumanKingdom)
            {
                city.gameObject.SetActive(false);
            }
            foreach (Transform city in allCities)
            {
                city.gameObject.SetActive(false);
            }
        }

        if(gameManager.instance.citiesAssigned == false)
        {
            AssignTownsAndCitiesToKingdoms();
            gameManager.instance.citiesAssigned = true; 
        }
    }
    void AssignTownsAndCitiesToKingdoms()
    {
        for(int i = 1; i <= 6; i++)
        {
            Vector3 coordinates = new Vector3(Random.Range(mapMinX, mapMaxX), 12, Random.Range(mapMinZ, mapMaxZ));
            cityPrefab.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", Color.red);
            GameObject city = Instantiate(cityPrefab, coordinates, Quaternion.identity);
            DontDestroyOnLoad(city);
            allCities.Add(city.transform);
            //AssignToKingdom(cityPrefab.transform.position, cityPrefab.transform, isCity: true);
        }
    }


    public void captureCity(Transform city)
    {
        MapCity cityToCapture = city.GetComponent<MapCity>();
        string kingdom = cityToCapture.GetCityKingdom(city);
        if(kingdom == "Dwarves")
        {
            citiesInDwarfKingdom.Remove(city.transform);
        }
        else if (kingdom == "Ogres")
        {
            citiesInOgreKingdom.Remove(city.transform);
        }
        else if (kingdom == "Elves")
        {
            citiesInElfKingdom.Remove(city.transform);
        }
        citiesInHumanKingdom.Add(city.transform);
        city.GetComponent<Renderer>().material.color = Color.blue;
        cityToCapture.ToggleCanvases();

    }

    void AssignToKingdom(Vector3 position, Transform objectTransform, bool isCity)
    {
        //if (position.x < 0 && position.z > 0)
        //{
        //    // Dwarves Kingdom (Top left)
        //    if (isCity) citiesInDwarfKingdom.Add(objectTransform);
        //    else townsInDwarfKingdom.Add(objectTransform);
        //}
        //else if (position.x >= 0 && position.z > 0)
        //{
        //    // Ogres Kingdom (Top right)
        //    if (isCity) citiesInOgreKingdom.Add(objectTransform);
        //    else townsInOgreKingdom.Add(objectTransform);
        //}
        //else if (position.x < 0 && position.z <= 0)
        //{
        //    // Elves Kingdom (Bottom left)
        //    if (isCity) citiesInElfKingdom.Add(objectTransform);
        //    else townsInElfKingdom.Add(objectTransform);
        //}
        //else if (position.x >= 0 && position.z <= 0)
        //{
        //    // Humans Kingdom (Bottom right)
        //    if (isCity) citiesInHumanKingdom.Add(objectTransform);
        //    else townsInHumanKingdom.Add(objectTransform);
        //}
        if (isCity)
        {
            citiesInDwarfKingdom.Add(objectTransform);
        }
        else
        {
            townsInDwarfKingdom.Add(objectTransform);
        }
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
            //     Debug.LogWarning($"No towns found for kingdom: {kingdom}");
            return null;
        }

        // Debug number of available towns for this kingdom
        //   Debug.Log($"Number of towns in {kingdom}: {townList.Count}");

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

    public void AssignColors()
    {
        foreach(Transform city in citiesInDwarfKingdom)
        {
            city.GetComponent<Renderer>().material.color = Color.gray;
        }
        foreach (Transform city in citiesInOgreKingdom)
        {
            city.GetComponent<Renderer>().material.color = Color.red;
        }
        foreach (Transform city in citiesInElfKingdom)
        {
            city.GetComponent<Renderer>().material.color = Color.yellow;
        }
        foreach (Transform city in citiesInHumanKingdom)
        {
            city.GetComponent<Renderer>().material.color = Color.blue;
        }
    }
    public bool IsCityInHumanKingdom(Transform city)
    {
        return citiesInHumanKingdom.Contains(city);
    }

    public void findNearestCity()
    { 
        float minDistance = float.MaxValue;
        GameObject[] cities = GameObject.FindGameObjectsWithTag("City");
        foreach (GameObject city in cities)
        {
            if (Vector3.Distance(mapPlayer.transform.position, city.transform.position) < minDistance)
            {
                minDistance = Vector3.Distance(mapPlayer.transform.position, city.transform.position);
                currentCity = city.transform;
            }
        }
        //currentCity.GetComponent<MapCity>().SpawnCaravanFromNearestCity(currentCity);
        DontDestroyOnLoad(currentCity);
        currentCity.gameObject.SetActive(false);
    }

    public void checkVictory()
    {
        //Cities to turn to win
        if(citiesInHumanKingdom.Count == 6)
        {
            playerWon = true;
        }
    }
}
