using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySpawner : MonoBehaviour
{
    GameObject toSpawn;
    GameObject spawner;

    bool hasSpawned;
    // Start is called before the first frame update
    void Start()
    {
        spawner = gameObject;
        AllyCombatManager.instance.SpawnerList.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnAlly()
    {
        if (!hasSpawned)
        {
            Instantiate(toSpawn, transform.position, transform.rotation);
            hasSpawned = true;
        }
    }

    // Etters

    public GameObject ToSpawn { get { return toSpawn; } set { toSpawn = value; } }
    public GameObject Spawner { get { return spawner; } set { spawner = value; } }
}
