using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySpawner : MonoBehaviour
{
    List<GameObject> toSpawn;

    // Start is called before the first frame update
    void Start()
    {
        toSpawn = AllyCombatManager.instance.CompanionList;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
