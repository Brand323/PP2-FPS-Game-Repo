using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AllyCombatManager : MonoBehaviour
{
    public static AllyCombatManager instance;
    int enemyArmySize;
    int allyArmySize;
    int currEnemyCount;

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

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GroupEnemy()
    {
        List<GameObject> list = null;
        if (allyArmySize > 3)
        {
            int allyCount = UnityEngine.Random.Range(1, 3);
            for (int i = 0; i < allyCount; i++)
            {
                int temp = UnityEngine.Random.Range(1, companionList.Count);
                list.Add(companionList[temp]);
                companionList.RemoveRange(temp, 1);
            }
        }
    }
}
