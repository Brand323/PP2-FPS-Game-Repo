using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] List<GameObject> meleeEnemies;
    GameObject meleeEnemy;
    [SerializeField] GameObject rangedEnemy;
    [Range(0f,1f)] [SerializeField] float spawnMeleePercentage;
    bool hasSpawned = false;
    bool hasAdded = false;
    int spawnCount=0;
    // Start is called before the first frame update
    void Start()
    {
        if (!hasAdded) StartCoroutine(AddToCombatManager());
    }
    public IEnumerator AddToCombatManager()
    {
        hasAdded = true;
        float rand = Random.Range(0f, .4f);
        yield return new WaitForSeconds(rand);
        if (CombatManager.instance != null)
        {
            CombatManager.instance.enemySpawnsList.Add(this);
        }

    }
    // Update is called once per frame
    public void AttemptSpawn()
    {
        if (!hasSpawned)
        {
            if (spawnCount < CombatManager.instance.enemyArmySize)
            {
                spawnCount++;
                float rand = Random.Range(0f,1f);
                SelectMeleeEnemy();
                if (rand < spawnMeleePercentage)
                {
                    Instantiate(meleeEnemy, transform.position, transform.rotation);

                } else
                {
                    Instantiate(rangedEnemy, transform.position, transform.rotation);
                }
                hasSpawned = true;
            } 
        }
    }
    public void SelectMeleeEnemy()
    {
        int randMelee= Random.Range(0,meleeEnemies.Count-1);
        meleeEnemy = meleeEnemies[randMelee];
    }

}