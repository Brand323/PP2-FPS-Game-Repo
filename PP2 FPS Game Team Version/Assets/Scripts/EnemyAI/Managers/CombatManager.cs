using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;
    [Range(1, 4)][SerializeField] int difficulty;
    [Range(1, 8)][SerializeField] int attackingPlayerMax;
    public int attackingPlayerCurr;
    public int enemiesExisting;
    public int enemyArmySize;
    public int playerArmySize;
    public List<EnemySpawn> enemySpawnsList;
    public bool hasSpawned = false;

    //Checks if fighting for city or not
    public bool isCityCombat = false;
    private Transform cityInCombat;
    public static Transform cityInCombatStatic;
    private string cityNameInCombat;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
/*            if (SceneManager.GetActiveScene().name == "CombatSceneArctic")
            {
                StartCoroutine(DelayedSpawn());
            }*/
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "CombatSceneArctic")
        {
            Debug.Log("City in combat (static): " + (cityInCombatStatic != null ? cityInCombatStatic.name : "null"));

            if (!string.IsNullOrEmpty(cityNameInCombat))
            {
                GameObject cityInScene = GameObject.Find(cityNameInCombat);
                if (cityInScene != null)
                {
                    cityInCombatStatic = cityInScene.transform;
                    Debug.Log("Reassigned cityInCombatStatic to: " + cityInScene.name);
                }
                else
                {
                    Debug.LogError("Could not find city by name: " + cityNameInCombat);
                }
            }

            Debug.Log("City in combat (static): " + (cityInCombatStatic != null ? cityInCombatStatic.name : "null"));

            ReassignPlayerForCombatScene();

            if (isCityCombat && cityInCombatStatic != null)
            {
                Debug.Log("Continuing city combat for city: " + cityInCombatStatic.name);
            }
            else
            {
                Debug.LogError("No city set for city combat.");
            }

            StartCoroutine(DelayedSpawn());
        }
    }
    private void ReassignPlayerForCombatScene()
    {
        //Debug.Log("Reassigning player for combat scene. City combat? " + isCityCombat);

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            FirstPersonController controller = player.GetComponent<FirstPersonController>();
            if (controller == null)
            {
             
                GameObject combatPlayerPrefab = Resources.Load<GameObject>("CombatPlayer");
                if (combatPlayerPrefab != null)
                {
                    Destroy(player);
                    GameObject newCombatPlayer = Instantiate(combatPlayerPrefab, Vector3.zero, Quaternion.identity);
                    gameManager.instance.playerScript = newCombatPlayer.GetComponent<FirstPersonController>();
                }
            }
            else
            {
                gameManager.instance.playerScript = controller;
            }
        }
        else
        {
            GameObject combatPlayerPrefab = Resources.Load<GameObject>("CombatPlayer");
            if (combatPlayerPrefab != null)
            {
                GameObject newCombatPlayer = Instantiate(combatPlayerPrefab, Vector3.zero, Quaternion.identity);
                gameManager.instance.playerScript = newCombatPlayer.GetComponent<FirstPersonController>();
            }
        }
    }


    void Start()
    {
        hasSpawned = false;
        if (SceneManager.GetActiveScene().name == "CombatSceneArctic")
        {
            StartCoroutine(DelayedSpawn());
        }
    }
    public void CheckToSpawn()
    {
            hasSpawned = false;
            StartCoroutine(DelayedSpawn());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public int GetDifficulty()
    {
        return difficulty;
    }

    public int GetAttackingPlayerMax()
    {
        return attackingPlayerMax;
    }
    public void SetDifficulty(int diff)
    {
        difficulty = diff;
        attackingPlayerMax = difficulty + 1;
    }
    public void SetCombatLogic(int enemyCount, int companionCount)
    {

    }
    public IEnumerator DelayedSpawn()
    {
        if (hasSpawned) { yield return null; } else
        {
            hasSpawned = true;
            yield return new WaitForSeconds(1);
            SpawnEnemies();
        }
    }
    public void CheckForVictory()
    {
        StartCoroutine(DelayedVictory());
    }
    public IEnumerator DelayedVictory()
    {
        yield return new  WaitForSeconds(0.1f);
        if (enemiesExisting > 0) { yield break; }
        UIManager.instance.victoryPopUp.SetActive(true);
        for (int i = 0; i < 20; i++) { 
            gameManager.instance.AddMoneyToPlayer(1);
            yield return new WaitForSeconds(0.03f);
        }
        if (gameManager.instance.isDefendingCaravan)
        {
            gameManager.instance.caravanArrived = true;
            gameManager.instance.isDefendingCaravan = false;
        }
        if (isCityCombat && cityInCombatStatic != null)
        {
            Debug.Log("City in combat: " + cityInCombatStatic.name);
            // Capture the city
            MapKingdomManager.Instance.CaptureCityForHumanKingdom(cityInCombatStatic);
        }
        else
        {
            Debug.LogError("No city set for city combat victory. CityInCombat is null.");
        }
        yield return new WaitForSeconds(3);
        UIManager.instance.victoryPopUp.SetActive(false);
        ClearSpawnerList();
        SceneManager.LoadScene("Map Scene");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void SpawnEnemies()
    {
        for (int i = 0; i < enemyArmySize; i++)
        {
            if (enemySpawnsList.Count == i)
            {
                break;
            }
            enemySpawnsList[i].AttemptSpawn();
        }
    }
    public void ClearSpawnerList()
    {
        if (enemySpawnsList.Count > 0)
        {
            enemySpawnsList.Clear();
        }
    }

    public void InitiateCityCombat(Transform city)
    {
        if (city == null)
        {
            Debug.LogError("InitiateCityCombat: City is null.");
            return;
        }

        isCityCombat = true;
        cityInCombat = city;
        cityInCombatStatic = city;
        cityNameInCombat = city.name;
        Debug.Log("Initiating combat for city: " + city.name);
    }

    public void InitiateArmyCombat()
    {
        isCityCombat = false;
        cityInCombat = null;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}