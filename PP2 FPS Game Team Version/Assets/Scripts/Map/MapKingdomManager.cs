using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapKingdomManager : MonoBehaviour
{
    public static MapKingdomManager instance;
    public List<Transform> townsInDwarfKingdom = new List<Transform>();
    public List<Transform> townsInOgreKingdom = new List<Transform>();
    public List<Transform> townsInElfKingdom = new List<Transform>();
    public List<Transform> townsInHumanKingdom = new List<Transform>();

    public List<Transform> citiesInDwarfKingdom = new List<Transform>();
    public List<Transform> citiesInOgreKingdom = new List<Transform>();
    public List<Transform> citiesInElfKingdom = new List<Transform>();
    public List<Transform> citiesInHumanKingdom = new List<Transform>();

    public float mapMinX = -100f;
    public float mapMaxX = 100f;
    public float mapMinZ = -100f;
    public float mapMaxZ = 100f;

    public static Transform currentCity;
    public Transform CurrentCity
    {
        get { return currentCity; }
        set { currentCity = value; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this);
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

        AssignTownsAndCitiesToKingdoms();
        AssignColors();
        giveNumberToCities();
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
            if (isCity) citiesInDwarfKingdom.Add(objectTransform);
            else townsInDwarfKingdom.Add(objectTransform);
        }
        else if (position.x >= 0 && position.z > 0)
        {
            // Ogres Kingdom (Top right)
            if (isCity) citiesInOgreKingdom.Add(objectTransform);
            else townsInOgreKingdom.Add(objectTransform);
        }
        else if (position.x < 0 && position.z <= 0)
        {
            // Elves Kingdom (Bottom left)
            if (isCity) citiesInElfKingdom.Add(objectTransform);
            else townsInElfKingdom.Add(objectTransform);
        }
        else if (position.x >= 0 && position.z <= 0)
        {
            // Humans Kingdom (Bottom right)
            if (isCity) citiesInHumanKingdom.Add(objectTransform);
            else townsInHumanKingdom.Add(objectTransform);
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

    private void giveNumberToCities()
    {
        int cityNumber = 1;
        foreach (Transform city in citiesInDwarfKingdom)
        {
            city.GetComponent<MapCity>().cityNumber = cityNumber;
            cityNumber++;
        }
        cityNumber = 1;
        foreach (Transform city in citiesInOgreKingdom)
        {
            city.GetComponent<MapCity>().cityNumber = cityNumber;
            cityNumber++;
        }
        cityNumber = 1;
        foreach (Transform city in citiesInElfKingdom)
        {
            city.GetComponent<MapCity>().cityNumber = cityNumber;
            cityNumber++;
        }
        cityNumber = 1;
        foreach (Transform city in citiesInHumanKingdom)
        {
            city.GetComponent<MapCity>().cityNumber = cityNumber;
            cityNumber++;
        }
    }

    public void captureCity(int cityNumber, string cityKingdom)
    {
        if(cityNumber == 1)
        {
            cityNumber = 2;
        }
        else
        {
            cityNumber = 1;
        }
        Transform city = null;
        if(cityKingdom == "Dwarves")
        {
            foreach(Transform _city in citiesInDwarfKingdom)
            {
                if(_city.gameObject.GetComponent<MapCity>().cityNumber == cityNumber)
                {
                    city = _city;
                }
            }
        }
        else if (cityKingdom == "Ogres")
        {
            foreach (Transform _city in citiesInOgreKingdom)
            {
                if (_city.gameObject.GetComponent<MapCity>().cityNumber == cityNumber)
                {
                    city = _city;
                }
            }
        }
        else if (cityKingdom == "Elves")
        {
            foreach (Transform _city in citiesInElfKingdom)
            {
                if (_city.gameObject.GetComponent<MapCity>().cityNumber == cityNumber)
                {
                    city = _city;
                }
            }
        }
        //MapCity cityToCapture = city.GetComponent<MapCity>();
        string kingdom = cityKingdom;
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
    }

    public bool IsCityInHumanKingdom(Transform city)
    {
        return citiesInHumanKingdom.Contains(city);
    }
}
