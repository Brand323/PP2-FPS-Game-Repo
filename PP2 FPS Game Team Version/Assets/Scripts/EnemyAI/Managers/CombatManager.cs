using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
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
            yield return new WaitForSeconds(2);
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
        for (int i = 0; i < 50; i++) { 
            gameManager.instance.AddMoneyToPlayer(1);
            UIManager.instance.moneyText.text = gameManager.instance.GetPlayerMoney().ToString();
            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Map Scene");
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

}