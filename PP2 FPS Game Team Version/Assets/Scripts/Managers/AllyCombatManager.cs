using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AllyCombatManager : MonoBehaviour
{
    public static AllyCombatManager instance;
    public int enemyArmySize;
    public int allyArmySize;
    public int currEnemyCount;
    public int currAllyCount;

    Vector3 enemyArmyPos;
    Vector3 allyArmyPos;

    public List<GameObject> enemyList;
    public List<GameObject> companionList;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AllyCombatManager.instance.enemyArmySize = CombatManager.instance.enemiesExisting;
        currEnemyCount = companionList.Count;
    }

    // Update is called once per frame
    void Update()
    {
        AllyCombatManager.instance.currEnemyCount = CombatManager.instance.enemiesExisting;
    }

    public void GroupEnemy()
    {
        List<GameObject> list = null;
        if (allyArmySize > 3)
        {
            int allyCount = UnityEngine.Random.Range(0, 4);
            for (int i = 0; i < allyCount; i++)
            {
                int temp = UnityEngine.Random.Range(0, companionList.Count);
                list.Add(companionList[temp]);
                companionList.RemoveRange(temp, 1);
            }
        }
    }

    public GameObject TargetEnemy()
    {
        if (enemyList.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, enemyList.Count);
            GameObject targetEnemy = enemyList[randomIndex];

            Debug.Log("Targeting enemy");
            return targetEnemy;
        }
        else
        {
            Debug.Log("No enemies to target");
            return null;
        }
    }
}