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
    int currAllyCount;

    Vector3 enemyArmyPos;
    Vector3 allyArmyPos;

    List<GameObject> enemyList;
    public List<GameObject> companionList;

    List<AllySpawner> spawnerList;

    public bool onBattleGround;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        spawnerList = new List<AllySpawner>();
        enemyList = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (onBattleGround)
        {
            for (int i = 0; i < companionList.Count; i++)
            {
                spawnerList[i].ToSpawn = companionList[i];
                spawnerList[i].SpawnAlly();
            }
            currEnemyCount = CombatManager.instance.enemiesExisting;
        }
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

            return targetEnemy;
        }
        else
            return null;
    }

    // Etters

    public List<GameObject> CompanionList { get { return companionList; } set { companionList = value; } }
    public List<GameObject> EnemyList { get { return enemyList; } set { enemyList = value; } }
    public List<AllySpawner> SpawnerList { get { return spawnerList; } set { spawnerList = value; } }
    public int EnemyArmySize { get { return enemyArmySize; } set { enemyArmySize = value; } }
    public int AllyArmySize { get { return allyArmySize; } set { allyArmySize = value; } }
    public Vector3 AllyArmyPosition { get { return allyArmyPos; } set { allyArmyPos = value; } }
    public Vector3 EnemyArmyPosition { get { return enemyArmyPos; } set { enemyArmyPos = value; } }
}