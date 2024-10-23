using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] GameObject HumanTemplate;
    [SerializeField] GameObject OgreTemplate;
    [SerializeField] GameObject DwarfTemplate;
    [SerializeField] GameObject ElfTemplate;

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
        CheckIfSpawnable();

        if (onBattleGround)
        {
         //   Debug.Log("Spawner List size: " + spawnerList.Count + " Companion List size: " + companionList.Count);
            for (int i = 0; i < companionList.Count; i++)
            {
                Debug.Log("i = " + i);
                spawnerList[i].ToSpawn = companionList[i];
                spawnerList[i].SpawnAlly();
            }
            if (enemyList.Count > 0)
            {
                currEnemyCount = CombatManager.instance.enemiesExisting;
            }
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

    public void CheckIfSpawnable()
    {
        if (SceneManager.GetActiveScene().name == "CombatSceneArctic")
            onBattleGround = true;
        else 
            onBattleGround = false;
    }

    public void RecruitMeleeCompanion()
    {
        int randomOption = UnityEngine.Random.Range(1, 4);

        switch (randomOption)
        {
            case 1:
                CompanionList.Add(HumanTemplate);
                break;
            case 2:
                CompanionList.Add(OgreTemplate);
                break;
            case 3:
                CompanionList.Add(DwarfTemplate);
                break;
        }

        allyArmySize += 1;
    }

    public void RecruitRangedCompanion()
    {
        CompanionList.Add(ElfTemplate);
        allyArmySize += 1;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "CombatSceneArctic")
        {
            // Clear the old spawners when the combat scene is loaded
            spawnerList.Clear();
            Debug.Log("Spawner List Cleared upon scene load");
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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