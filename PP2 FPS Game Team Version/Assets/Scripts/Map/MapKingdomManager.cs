using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapKingdomManager : MonoBehaviour
{
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



    // Start is called before the first frame update
    void Start()
    {
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
        switch (kingdom)
        {
            case "Dwarves":
                if (townsInDwarfKingdom.Count > 0)
                    return townsInDwarfKingdom[Random.Range(0, townsInDwarfKingdom.Count)];
                break;
            case "Ogres":
                if (townsInOgreKingdom.Count > 0)
                    return townsInOgreKingdom[Random.Range(0, townsInOgreKingdom.Count)];
                break;
            case "Elves":
                if (townsInElfKingdom.Count > 0)
                    return townsInElfKingdom[Random.Range(0, townsInElfKingdom.Count)];
                break;
            case "Humans":
                if (townsInHumanKingdom.Count > 0)
                    return townsInHumanKingdom[Random.Range(0, townsInHumanKingdom.Count)];
                break;
        }

        return null;
    }

    public bool IsCityInHumanKingdom(Transform city)
    {
        return citiesInHumanKingdom.Contains(city);
    }
}
