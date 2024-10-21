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
    public bool wonBattle;

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
        if (hasSpawned) { yield return null; }
        else
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
        yield return new WaitForSeconds(0.1f);
        if (enemiesExisting > 0) { yield break; }
        UIManager.instance.victoryPopUp.SetActive(true);
        for (int i = 0; i < 20; i++)
        {
            gameManager.instance.AddMoneyToPlayer(1);
            yield return new WaitForSeconds(0.03f);
        }
        if(gameManager.instance.inBattleForCity)
        {
            MapKingdomManager.instance.captureCity(MapKingdomManager.instance.currentCity);
            MapKingdomManager.instance.checkVictory();
            //Destroy(MapKingdomManager.instance.CurrentCity.gameObject);
            //wonBattle = true;
            gameManager.instance.inBattleForCity = false;
        }
        if (!gameManager.instance.isDefendingCaravan && gameManager.instance.isQuestInProgress)
        {
            gameManager.instance.isQuestInProgress = false;
        }
        if (gameManager.instance.isDefendingCaravan)
        {
            gameManager.instance.caravanArrived = true;
            gameManager.instance.isDefendingCaravan = false;
        }
        AudioManager.instance.fadeEnded = false;
        AudioManager.instance.fadeOut();
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

    public void exitToMap()
    {
        enemiesExisting = 0;
        ClearSpawnerList();
        SceneManager.LoadScene("Map Scene");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}